﻿using Microsoft.Extensions.Caching.Distributed;
using MovieModel;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Services.Storage
{
    public class RedisStorage<TItem> : IStorage<TItem> where TItem : IStorageKey
    {
        private readonly IDistributedCache _redisCache;
        private readonly IRedisConnectionFactory _factory;

        public RedisStorage(IDistributedCache redisCache, IRedisConnectionFactory factory)
        {
            _redisCache = redisCache;
            _factory = factory;
        }

        public async Task<TItem> Get(string key)
        {
            var stringObj = await _redisCache.GetStringAsync(BuildKey(key));
            if (!string.IsNullOrEmpty(stringObj))
            {
                return JsonConvert.DeserializeObject<TItem>(stringObj);
            }

            return default;
        }

        public async Task Set(TItem item)
        {
            var serializedStringObj = JsonConvert.SerializeObject(item);
            var byteArray = Encoding.UTF8.GetBytes(serializedStringObj);

            await _redisCache.SetAsync(BuildKey(item.Key), byteArray);
        }

        public async Task<TItem> GetOrSet(string key, TItem item)
        {
            var byteArrayObj = await _redisCache.GetAsync(key);

            if (byteArrayObj != null && byteArrayObj.Length > 0)
            {
                var stringObj = Encoding.UTF8.GetString(byteArrayObj);
                return JsonConvert.DeserializeObject<TItem>(stringObj);
            }

            var serializedStringObj = JsonConvert.SerializeObject(item);
            var byteArray = Encoding.UTF8.GetBytes(serializedStringObj);

            await _redisCache.SetAsync(BuildKey(key), byteArray);
            return item;
        }

        public async Task<IEnumerable<TItem>> GetBatch(IEnumerable<string> keys)
        {
            var redis = await _factory.ConnectAsync();
            var batch = redis.GetDatabase().CreateBatch();

            var testTasks = batch.StringGetAsync(keys.Select(x => new RedisKey(BuildKey(x))).ToArray());
            var result1 = await Task.WhenAll(testTasks);


            var getRedisBatchTasks = keys.Select(x => batch.StringGetAsync(new RedisKey(BuildKey(x))));

            batch.Execute();

            var result = await Task.WhenAll(getRedisBatchTasks);

            return result.Select(x => JsonConvert.DeserializeObject<TItem>(x));
        }

        public async Task SetBatch(IEnumerable<TItem> items)
        {
            var redis = await _factory.ConnectAsync();
            var batch = redis.GetDatabase().CreateBatch();

            var setRedisBatchTasks = items
                .Select(x => batch.StringSetAsync(
                    new RedisKey(BuildKey(x.Key)), new RedisValue(JsonConvert.SerializeObject(x)), TimeSpan.FromMinutes(5)));
            
            batch.Execute();

            await Task.WhenAll(setRedisBatchTasks);
        }

        public Task SetPipeline(IEnumerable<TItem> items)
        {
            throw new NotImplementedException();
        }

        public string BuildKey(string key) => $"{typeof(TItem).Name}_{key}";
    }
}
