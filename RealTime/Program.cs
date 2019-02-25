using BenchmarkDotNet.Running;
using System;

namespace RealTime
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Benchmark>();            

            //Console.WriteLine(new Benchmark().Realitme());
            //Console.WriteLine(new Benchmark().NonRealitme());
        }
    }
}
