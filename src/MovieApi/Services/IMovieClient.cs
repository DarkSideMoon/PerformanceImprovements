using MovieModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Services
{
    public interface IMovieClient
    {
        Task<bool> SetMovie(Movie movie);

        Task<Movie> GetMovie(int id);

        Task<bool> SetBatchMovie(IEnumerable<Movie> movie);

        Task<IEnumerable<Movie>> GetBatchMovie(int id);
    }
}
