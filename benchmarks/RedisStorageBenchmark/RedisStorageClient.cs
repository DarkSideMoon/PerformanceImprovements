using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Options;
using MovieApi.Configuration;
using MovieApi.Services.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisStorageBenchmark
{
    public interface IRedisStorageClient
    {
        Task PingPipelineAsync();

        Task PingAsync();
    }

    [MemoryDiagnoser]
    public class RedisStorageClient : IRedisStorageClient
    {
        private IRedisConnectionFactory _redisConnectionFactory;

        [Params(10, 100, 1000, 10000)]
        public int CountOfPing { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _redisConnectionFactory = new RedisConnectionFactory(Options.Create(new RedisConfiguration { ConnectionString = "localhost:6379" }));
        }

        [Benchmark]
        public async Task PingAsync()
        {
            var redis = await _redisConnectionFactory.ConnectAsync();
            var redisDb = redis.GetDatabase();

            for (int i = 0; i < CountOfPing; i++)
            {
                await redisDb.PingAsync();
            }
        }

        [Benchmark]
        public async Task PingPipelineAsync()
        {
            var redis = await _redisConnectionFactory.ConnectAsync();
            var redisDb = redis.GetDatabase();

            var pingRedisTasks = new List<Task>();
            for (int i = 0; i < CountOfPing; i++)
            {
                pingRedisTasks.Add(redisDb.PingAsync());
            }

            await Task.WhenAll(pingRedisTasks);
        }

        [Benchmark]
        public async Task PingBatchAsync()
        {
            var redis = await _redisConnectionFactory.ConnectAsync();
            var batch = redis.GetDatabase().CreateBatch();

            var pingRedisTasks = new List<Task>();
            for (int i = 0; i < CountOfPing; i++)
            {
                pingRedisTasks.Add(batch.PingAsync());
            }

            batch.Execute();

            await Task.WhenAll(pingRedisTasks);
        }
    }
}
