using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace RealTime
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmark>();
        }
    }
}
