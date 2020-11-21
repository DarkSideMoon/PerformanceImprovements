using BenchmarkDotNet.Attributes;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using MovieGrpcClient;
using static MovieGrpcClient.Greeter;

namespace GrpcBenchmark
{
    public interface IGrpcClient
    {
        Task SayHello();
    }

    [MemoryDiagnoser]
    public class GrpcClient : IGrpcClient
    {
        private GrpcChannel channel;
        private static GreeterClient client;

        [GlobalSetup]
        public void Setup()
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
            client = new Greeter.GreeterClient(channel);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
        }

        [Benchmark]
        public async Task SayHello()
        {
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "World" });
            Console.WriteLine("Greeting: " + reply.Message);
        }
    }
}
