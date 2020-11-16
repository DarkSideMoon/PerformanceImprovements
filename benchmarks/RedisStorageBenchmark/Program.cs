using BenchmarkDotNet.Running;

namespace RedisStorageBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<RedisStorageClient>();
        }
    }
}
