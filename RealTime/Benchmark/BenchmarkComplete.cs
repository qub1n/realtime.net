using BenchmarkDotNet.Attributes;
using System;
using System.Buffers;
using System.IO;
using System.Linq;

namespace RealTime.Benchmark
{
    [MemoryDiagnoser]
    public class BenchmarkComplete
    {
        const string TestFile = "TestFiles/animals.txt";
        LegServiceSpan _legServiceSpan = new LegServiceSpan();
        LegServiceStringFast _legServiceStringFast = new LegServiceStringFast();

        public BenchmarkComplete()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(TestFile));
            // cca 100 KB of text
            string content = string.Concat(Enumerable.Repeat("dog,cat,spider,cat,bird,", 4000));
            File.WriteAllText(TestFile, content);
        }

        [Benchmark]
        public void ParseFileOnPool() => ParseFileOnPool(TestFile);

        [Benchmark]
        public void ParseFileOnHeap() => ParseFileOnHeap(TestFile);

        [Benchmark]
        public void ParseFileOnStack() => ParseFileOnStack(TestFile);

        public void ParseFileOnHeap(string filename)
        {
            string animals = File.ReadAllText(filename);           
            int legs = _legServiceStringFast.NumberOfLegs(animals);
        }

        public void ParseFileOnStack(string filename)
        {
            Span<char> buffer = stackalloc char[1024 * 200]; // 200KB is might be OK on stack
            using (var stream = File.OpenRead(filename))
            {
                StreamReader reader = new StreamReader(stream);
                int count = reader.ReadBlock(buffer);
                if (count == buffer.Length)
                    throw new Exception($"Buffer size {buffer.Length} too small, use array pooling.");
                buffer = buffer.Slice(0, count);

                int legs = _legServiceSpan.NumberOfLegs(buffer);
            }
        }

        public void ParseFileOnPool(string filename)
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

                    Span<char> span = new Span<char>(buffer).Slice(0, count);

                    int legs = _legServiceSpan.NumberOfLegs(span);
                }
                finally
                {
                    pool.Return(buffer);
                }
            }
        }
    }
}
