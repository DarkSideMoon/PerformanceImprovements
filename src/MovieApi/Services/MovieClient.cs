using MovieApi.Services.Storage;
using MovieModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    public class MovieClient : IMovieClient
    {
        private readonly IStorage<Movie> _storageMovie;

        public MovieClient(IStorage<Movie> storageMovie)
        {
            _storageMovie = storageMovie;
        }

        public async Task<IEnumerable<Movie>> GetBatchMovie(IEnumerable<string> keys) => await _storageMovie.GetBatch(keys);

        public async Task<Movie> GetMovie(string key) => await _storageMovie.Get(key);

        public async Task SetBatchMovie(IEnumerable<Movie> movies) => await _storageMovie.SetBatch(movies);

        public async Task SetMovie(Movie movie) => await _storageMovie.Set(movie);

        public async Task PingPipelineRedisAsync(int countOfPing) => await _storageMovie.PingPipelineAsync(countOfPing);

        public async Task PingRedisAsync(int countOfPing) => await _storageMovie.PingAsync(countOfPing);
    }
}
