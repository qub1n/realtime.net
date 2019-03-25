using BenchmarkDotNet.Attributes;
using System.Linq;
using LegCounterService.Service;

namespace Veeam.Meetup.Benchmark
{
    [HtmlExporter]
    [MemoryDiagnoser]
    public class BenchmarkParse
    {
        #region services

        private readonly LegServiceSpan _legServiceSpan = new LegServiceSpan();
        private readonly LegServiceString _legServiceString = new LegServiceString();
        private readonly LegServiceMemoryForEach _memoryLegServiceForEach = new LegServiceMemoryForEach();
        private readonly LegServiceMemoryDelegate _memoryLegServiceDelegate = new LegServiceMemoryDelegate();
        private readonly LegServiceStringFast _legServiceStringFast = new LegServiceStringFast();

        #endregion region
        private readonly string _data;
        public BenchmarkParse()
        {
            _data = string.Concat(Enumerable.Repeat("dog,cat,spider,cat,bird,", 1000));
        }

        [Benchmark(Baseline = true)]
        public int LegsString() => _legServiceString.NumberOfLegs(_data);

        [Benchmark]
        public int LegsSpan() => _legServiceSpan.NumberOfLegs(_data);

        [Benchmark]
        public int LegsStringFast() => _legServiceStringFast.NumberOfLegs(_data);


        //[Benchmark]
        public int LegMemory() => _memoryLegServiceDelegate.NumberOfLegs(_data);

        //[Benchmark]
        //public int LegMemoryForEach() => memoryLegServiceForEach.NumberOfLegs(_data);       
    }
}
