using BenchmarkDotNet.Attributes;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using MovieModel;
using System.Collections.Generic;

namespace HttpCompletionOptionBenchmark
{
    public interface IMovieClient
    {
        Task GetMovieAsyncWithoutHttpCompletionOption();
        Task GetMovieAsyncWithHttpCompletionOption();

        Task GetMovieAsyncWithoutHttpCompletionOptionNewtonsoft();
        Task GetMovieAsyncWithHttpCompletionOptionNewtonsoft();

        Task GetMovieAsyncWithoutHttpCompletionOptionNewtonsoftUtf8Json();
        Task GetMovieAsyncWithHttpCompletionOptionNewtonsoftUtf8Json();
    }

    [MemoryDiagnoser]
    public class MovieClient : IMovieClient
    {
        private const string BaseMovieUrl = "http://localhost:5000/";
        private const string GetMovieUrl = "Performance/movies";

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
        public async Task GetMovieAsyncWithoutHttpCompletionOption()
        {
            var response = await _httpClient.GetAsync(GetMovieUrl);
            response.EnsureSuccessStatusCode();

            try
            {
                var content = await response.Content.ReadAsStreamAsync();
                var result = System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<Movie>>(content);
            }
            finally
            {
                response.Dispose();
            }
        }

        [Benchmark]
        public async Task GetMovieAsyncWithHttpCompletionOption()
        {
            var response = await _httpClient.GetAsync(GetMovieUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            try
            {
                var content = await response.Content.ReadAsStreamAsync();
                var result = System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<Movie>>(content);
            }
            finally
            {
                response.Dispose();
            }
        }

        //[Benchmark]
        public async Task GetMovieAsyncWithoutHttpCompletionOptionNewtonsoft()
        {
            var response = await _httpClient.GetAsync(GetMovieUrl);
            response.EnsureSuccessStatusCode();

            try
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Movie>>(content);
            }
            finally
            {
                response.Dispose();
            }
        }

        //[Benchmark]
        public async Task GetMovieAsyncWithHttpCompletionOptionNewtonsoft()
        {
            var response = await _httpClient.GetAsync(GetMovieUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            try
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Movie>>(content);
            }
            finally
            {
                response.Dispose();
            }
        }

        [Benchmark]
        public async Task GetMovieAsyncWithoutHttpCompletionOptionNewtonsoftUtf8Json()
        {
            var response = await _httpClient.GetAsync(GetMovieUrl);
            response.EnsureSuccessStatusCode();

            try
            {
                var content = await response.Content.ReadAsStreamAsync();
                var result = Utf8Json.JsonSerializer.DeserializeAsync<IEnumerable<Movie>>(content);
            }
            finally
            {
                response.Dispose();
            }
        }

        [Benchmark]
        public async Task GetMovieAsyncWithHttpCompletionOptionNewtonsoftUtf8Json()
        {
            var response = await _httpClient.GetAsync(GetMovieUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            try
            {
                var content = await response.Content.ReadAsStreamAsync();
                var result = Utf8Json.JsonSerializer.DeserializeAsync<IEnumerable<Movie>>(content);
            }
            finally
            {
                response.Dispose();
            }
        }
    }
}
