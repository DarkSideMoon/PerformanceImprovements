using BenchmarkDotNet.Attributes;
using Grpc.Net.Client;
using System.Threading.Tasks;
using MovieGrpc;
using static MovieGrpc.Greeter;
using static MovieGrpc.MovieService;

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
        private GreeterClient client;

        private GrpcChannel channelMovie;
        private MovieServiceClient movieClient;

        /// <summary>
        /// Install certificate dotnet dev-certs https --trust
        /// https://www.hanselman.com/blog/developing-locally-with-aspnet-core-under-https-ssl-and-selfsigned-certs
        /// https://www.dotnetcurry.com/aspnet-core/1514/grpc-asp-net-core-3
        /// https://docs.microsoft.com/en-us/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-5.0&tabs=visual-studio
        /// </summary>
        [GlobalSetup]
        public void Setup()
        {
            // If you don't generate certificate -> use this code
            //var httpHandler = new HttpClientHandler();
            //// Return `true` to allow certificates that are untrusted/invalid
            //httpHandler.ServerCertificateCustomValidationCallback =
            //    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            channel = GrpcChannel.ForAddress("https://localhost:5001"); // new GrpcChannelOptions { HttpHandler = httpHandler }
            client = new Greeter.GreeterClient(channel);

            channelMovie = GrpcChannel.ForAddress("https://localhost:5001"); // new GrpcChannelOptions { HttpHandler = httpHandler }
            movieClient = new MovieServiceClient(channelMovie);
        }

        [GlobalCleanup]
        public void Cleanup()
        {
        }

        [Benchmark]
        public async Task SayHello()
        {
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "World" });
        }

        [Benchmark]
        public async Task GrpcMovie()
        {
            var response = await movieClient.GetMovieAsync(new MovieRequest());
        }

        [Benchmark]
        public async Task GrpcMovies()
        {
            var response = await movieClient.GetMoviesAsync(new MovieRequest());
        }
    }
}
