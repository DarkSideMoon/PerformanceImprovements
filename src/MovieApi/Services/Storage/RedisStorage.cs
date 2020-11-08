using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Services.Storage
{
    public class RedisStorage<TItem> : IStorage<TItem>
    {
        private readonly IDistributedCache _redisCache;

        public RedisStorage(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public Task<TItem> Get(string key)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TItem>> GetBatch(string key)
        {
            throw new NotImplementedException();
        }

        public Task<TItem> GetOrSet(string key, TItem item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TItem>> GetOrSetBatch(string key, IEnumerable<TItem> item)
        {
            throw new NotImplementedException();
        }

        public Task Set(string key, TItem item)
        {
            throw new NotImplementedException();
        }

        public Task SetBatch(string key, IEnumerable<TItem> items)
        {
            throw new NotImplementedException();
        }
    }
}
