using BenchmarkDotNet.Attributes;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using MovieGrpcClient;
using static MovieGrpcClient.Greeter;
using System.Net.Http;

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

        /// <summary>
        /// Install certificate dotnet dev-certs https --trust
        /// https://www.hanselman.com/blog/developing-locally-with-aspnet-core-under-https-ssl-and-selfsigned-certs
        /// https://www.dotnetcurry.com/aspnet-core/1514/grpc-asp-net-core-3
        /// https://docs.microsoft.com/en-us/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-5.0&tabs=visual-studio
        /// </summary>
        [GlobalSetup]
        public void Setup()
        {
            //var httpHandler = new HttpClientHandler();
            //// Return `true` to allow certificates that are untrusted/invalid
            //httpHandler.ServerCertificateCustomValidationCallback =
            //    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            channel = GrpcChannel.ForAddress("https://localhost:5001"); // new GrpcChannelOptions { HttpHandler = httpHandler }
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
            //Console.WriteLine("Greeting: " + reply.Message);
        }
    }
}
