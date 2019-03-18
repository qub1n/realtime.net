using BenchmarkDotNet.Running;
using RealTime.Benchmark;
using System;

namespace RealTime
{
    class Program
    {
        static void Main(string[] args)
        {          
            BenchmarkRunner.Run<BenchmarkComplete>();
            BenchmarkRunner.Run<BenchmarkRead>();
            BenchmarkRunner.Run<BenchmarkParse>();
        }
    }
}
