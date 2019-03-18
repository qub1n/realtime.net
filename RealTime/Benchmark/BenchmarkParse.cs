using System;
using BenchmarkDotNet.Attributes;
using System.Security.Cryptography;
using System.Linq;

namespace RealTime
{
    [HtmlExporter]
    [MemoryDiagnoser]
    //[AllStatisticsColumn]
    public class BenchmarkParse
    {

        private readonly LegServiceSpan legServiceSpan = new LegServiceSpan();
        private readonly LegServiceString nonrealitme = new LegServiceString();
        private readonly LegServiceMemoryForEach memoryLegServiceForEach = new LegServiceMemoryForEach();
        private readonly LegServiceMemoryDelegate memoryLegServiceDelegate = new LegServiceMemoryDelegate();
        private readonly LegServiceStringFast legServiceStringFast = new LegServiceStringFast();

        private readonly string _data;

        public BenchmarkParse()
        {        
            _data = string.Concat(Enumerable.Repeat("dog,cat,spider,cat,bird,", 1000));
        }

        [Benchmark]
        public int LegsSpan() => legServiceSpan.NumberOfLegs(_data);

        [Benchmark(Baseline = true)]
        public int LegsString() => nonrealitme.NumberOfLegs(_data);

        [Benchmark]
        public int LegMemory() => memoryLegServiceDelegate.NumberOfLegs(_data);

        [Benchmark]
        public int LegsStringFast() => legServiceStringFast.NumberOfLegs(_data);

        //[Benchmark]
        //public int LegMemoryForEach() => memoryLegServiceForEach.NumberOfLegs(_data);       
    }
}
