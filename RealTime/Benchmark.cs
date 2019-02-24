using System;
using BenchmarkDotNet.Attributes;
using System.Security.Cryptography;

namespace RealTime
{
    public class Benchmark
    {

        private readonly RealTimeService realitme = new RealTimeService();
        private readonly NonRealTimeService nonrealitme = new NonRealTimeService();

        private readonly string _data;

        public Benchmark()
        {
            _data = "dog;cat";
        }

        [Benchmark]
        public int Realitme() => realitme.GetValue(_data);

        [Benchmark]
        public int NonRealitme() => nonrealitme.GetValue(_data);
    }
}
