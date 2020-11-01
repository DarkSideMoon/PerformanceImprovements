using MovieModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    public class MovieClient : IMovieClient
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
