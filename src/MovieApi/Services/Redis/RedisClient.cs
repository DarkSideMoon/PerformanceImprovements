using MovieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Services.Redis
{
    public class RedisClient : IRedisClient
    {
        public Task<IEnumerable<Movie>> GetBatchMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovie(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetBatchMovie(IEnumerable<Movie> movie)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
