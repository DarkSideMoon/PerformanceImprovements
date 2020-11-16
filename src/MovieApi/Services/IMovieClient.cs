using MovieModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    public interface IMovieClient
    {
        Task SetMovie(Movie movie);

        Task<Movie> GetMovie(string key);

        Task SetBatchMovie(IEnumerable<Movie> movies);

        Task<IEnumerable<Movie>> GetBatchMovie(IEnumerable<string> keys);

        Task PingPipelineRedisAsync(int countOfPing);

        Task PingRedisAsync(int countOfPing);
    }
}
