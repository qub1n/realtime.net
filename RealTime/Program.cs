using BenchmarkDotNet.Running;
using System;

namespace RealTime
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmark>();

            //Console.WriteLine(new Benchmark().LegsString());
            //Console.WriteLine(new Benchmark().LegsStringFast());
            //Console.WriteLine(new Benchmark().LegsSpan());
            //Console.WriteLine(new Benchmark().LegMemory());
            //Console.WriteLine(new Benchmark().LegMemory2());
        }
    }
}
