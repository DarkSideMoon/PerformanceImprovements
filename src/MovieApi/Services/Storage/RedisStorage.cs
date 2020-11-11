using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Services.Storage
{
    public class RedisStorage<String> : IStorage<string>
    {
        private readonly IDistributedCache _redisCache;
        private readonly IRedisConnectionFactory _factory;

        public RedisStorage(IDistributedCache redisCache, IRedisConnectionFactory factory)
        {
            _redisCache = redisCache;
            _factory = factory;
        }

        public async Task<string> Get(string key) => await _redisCache.GetStringAsync(key);

        public async Task Set(string key, string item) => await _redisCache.SetStringAsync(key, item);

        public async Task<string> GetOrSet(string key, string item)
        {
            var value = await _redisCache.GetStringAsync(key);

            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }

            await _redisCache.SetStringAsync(key, item);
            return item;
        }

        public async Task<IEnumerable<string>> GetBatch(IEnumerable<string> keys)
        {
            var redis = await _factory.ConnectAsync();
            var batch = redis.GetDatabase().CreateBatch();

            var getRedisBatchTasks = keys.Select(x => batch.StringGetAsync(new RedisKey(x)));
            var result = await Task.WhenAll(getRedisBatchTasks);

            return result.Select(x => x.ToString());
        }

        public async Task SetBatch(string key, IEnumerable<string> items)
        {
            var redis = await _factory.ConnectAsync();
            var batch = redis.GetDatabase().CreateBatch();

            var setRedisBatchTasks = items
                .Select(x => batch.StringSetAsync(
                    new RedisKey(key), new RedisValue(x), TimeSpan.FromMinutes(5), when: When.Always, flags: CommandFlags.FireAndForget));

            await Task.WhenAll(setRedisBatchTasks);
        }
    }
}
