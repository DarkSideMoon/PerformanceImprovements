using BenchmarkDotNet.Attributes;
using MovieModel;
using MovieModel.Rest;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestBenchmark
{
    public interface IRestClient
    {
        Task SayGreet();
    }

    [MemoryDiagnoser]
    public class RestClient : IRestClient
    {
        private const string BaseMovieUrl = "http://localhost:5000/";
        private const string GreeterUrl = "Greeter/Test";
        private const string PerformanceMovieBaseUrl = "Performance";

        private HttpClient _httpClient;

        [GlobalSetup]
        public void Setup()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseMovieUrl)
            };
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _httpClient.Dispose();
        }

        [Benchmark]
        public async Task SayGreet()
        {
            var response = await _httpClient.GetAsync(GreeterUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<HelloResponse>(content);
        }

        [Benchmark]
        public async Task RestPerformanceMovie()
        {
            var response = await _httpClient.GetAsync($"{PerformanceMovieBaseUrl}/movie");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Movie>(content);
        }

        [Benchmark]
        public async Task RestPerformanceMovies()
        {
            var response = await _httpClient.GetAsync($"{PerformanceMovieBaseUrl}/movies");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Movie>>(content);
        }
    }
}
