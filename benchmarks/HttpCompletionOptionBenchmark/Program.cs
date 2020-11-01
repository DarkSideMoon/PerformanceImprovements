using BenchmarkDotNet.Running;

namespace HttpCompletionOptionBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<MovieClient>();
        }
    }
}
