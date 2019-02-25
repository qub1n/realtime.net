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

        private readonly RealTimeService realitme = new RealTimeService();
        private readonly NonRealTimeService nonrealitme = new NonRealTimeService();

        private readonly string _data;

        public Benchmark()
        {
            _data = string.Concat(Enumerable.Repeat("dog,cat,spider,cat,bird,", 1000));
        }

        [Benchmark]
        public int Realitme() => realitme.NumberOfLegs(_data);

        [Benchmark]
        public int NonRealitme() => nonrealitme.NumberOfLegs(_data);
    }
}
