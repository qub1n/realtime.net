using BenchmarkDotNet.Attributes;
using System;
using System.Threading.Tasks;

namespace RealTime.Benchmark
{
    [HtmlExporter]
    [MemoryDiagnoser]
    public class BenchMarkCompleteParallel
    {
        private readonly BenchmarkComplete _benchMark = new BenchmarkComplete();
        const int Degree = 500;
        ParallelOptions options = new ParallelOptions()
        {
            MaxDegreeOfParallelism = 50,
        };

        [Benchmark]
        public void ParseFileOnPool() => InParallel(() => _benchMark.ParseFileOnPool());

        [Benchmark]
        public void ParseFileOnHeap() => InParallel(() => _benchMark.ParseFileOnHeap());

        [Benchmark]
        public void ParseFileOnStack() => InParallel(() => _benchMark.ParseFileOnStack());

        private void InParallel(Action action)
        {
            Parallel.For(0, Degree, options, (currentFile) =>
            {
                action();
            });
        }
    }
}