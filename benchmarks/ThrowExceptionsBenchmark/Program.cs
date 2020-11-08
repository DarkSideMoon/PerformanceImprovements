using BenchmarkDotNet.Running;

namespace ThrowExceptionsBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ThrowExceptionsClient>();
        }
    }
}
