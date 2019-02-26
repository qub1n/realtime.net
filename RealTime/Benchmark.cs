using System;
using BenchmarkDotNet.Attributes;
using System.Security.Cryptography;
using System.Linq;

namespace RealTime
{
    [HtmlExporter]
    [MemoryDiagnoser]
    //[AllStatisticsColumn]
    public class Benchmark
    {

        private readonly LegServiceSpan legServiceSpan = new LegServiceSpan();
        private readonly LegServiceString nonrealitme = new LegServiceString();
        private readonly LegServiceMemoryForEach memoryLegService = new LegServiceMemoryForEach();
        private readonly LegServiceMemoryDelegate memoryLegService2 = new LegServiceMemoryDelegate();

        private readonly string _data;

        public Benchmark()
        {
            _data = string.Concat(Enumerable.Repeat("dog,cat,spider,cat,bird,", 1000));
        }

        [Benchmark]
        public int LegsSpan() => legServiceSpan.NumberOfLegs(_data);

        [Benchmark]
        public int LegsString() => nonrealitme.NumberOfLegs(_data);

        [Benchmark]
        public int LegMemory() => memoryLegService.NumberOfLegs(_data);

        [Benchmark]
        public int LegMemory2() => memoryLegService2.NumberOfLegs(_data);
    }
}
