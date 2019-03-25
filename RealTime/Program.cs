using BenchmarkDotNet.Running;
using Veeam.Meetup.Benchmark;

namespace Veeam.Meetup
{
    class Program
    {
        static void Main(string[] args)
        {                      
            BenchmarkRunner.Run<BenchmarkRead>();
            BenchmarkRunner.Run<BenchmarkParse>();
            BenchmarkRunner.Run<BenchmarkComplete>();
        }
    }
}
