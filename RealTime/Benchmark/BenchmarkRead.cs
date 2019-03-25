using BenchmarkDotNet.Attributes;
using System;
using System.Buffers;
using System.IO;
using System.Linq;

namespace Veeam.Meetup.Benchmark
{
    [HtmlExporter]
    [MemoryDiagnoser]
    public class BenchmarkRead
    {
        const string TestFile = "TestFiles/animals.txt";

        public BenchmarkRead()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(TestFile));
            // cca 100 KB of text
            string content = string.Concat(Enumerable.Repeat("dog,cat,spider,cat,bird,", 4000));
            File.WriteAllText(TestFile, content);
        }

        [Benchmark]
        public void ReadFileOnPool() => ReadFileOnPool(TestFile);

        [Benchmark]
        public void ReadFileOnHeap() => ReadFileOnHeap(TestFile);

        [Benchmark]
        public void ReadFileOnStack() => ReadFileOnStack(TestFile);

        public void ReadFileOnHeap(string filename)
        {
            string text = File.ReadAllText(filename);
            // ....call parse
        }

        public void ReadFileOnStack(string filename)
        {
            Span<char> buffer = stackalloc char[1024 * 200]; // 200KB is might be OK on stack
            using (var reader = File.OpenText(filename))
            {
                int count = reader.ReadBlock(buffer);
                if (count == buffer.Length)
                    throw new Exception($"Buffer size {buffer.Length} too small, use array pooling.");
                buffer = buffer.Slice(0, count);
                // ....call parse
            }
        }

        public void ReadFileOnPool(string filename)
        {
            ArrayPool<char> pool = ArrayPool<char>.Shared;
            using (var stream = File.OpenRead(filename))
            {
                long len = stream.Length;
                char[] buffer = pool.Rent((int)len);
                try
                {
                    StreamReader reader = new StreamReader(stream);
                    int count = reader.ReadBlock(buffer);
                    if (count != len)
                        throw new Exception($"{count} != {len}");

                    var span = new Span<char>(buffer).Slice(0, count);
                    // ....call parse
                }
                finally
                {
                    pool.Return(buffer);
                }
            }
        }
    }
}
