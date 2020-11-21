using BenchmarkDotNet.Running;

namespace RestBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<RestClient>();
        }
    }
}
