using Grpc.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieGrpc.Services
{
    public class MovieServiceGrpc : MovieService.MovieServiceBase
    {
        public MovieServiceGrpc()
        {
        }

        public override Task<MovieResponse> GetMovie(MovieRequest request, ServerCallContext context)
        {
            return Task.FromResult(new MovieResponse
            {
                Id = 1,
                ImdbId = "2",
                Budget = 1000,
                Revenue = 100000,
                Popularity = 10,
                TagLine = "test",
                VoteAverage = 5,
                VoteCount = 1000,
                OriginalLanguage = "ua",
                OriginalTitle = "Test",
                Title = "Test Cool",
                Overview = "Cool",
                ReleaseDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                State = "Released"
            });
        }

        public override Task<MoviesResponse> GetMovies(MovieRequest request, ServerCallContext context)
        {
            var result = new MoviesResponse();
            result.Movies.AddRange(Enumerable.Range(200, 200)
                .Select(index => new MovieResponse
                {
                    Id = 1,
                    ImdbId = "2",
                    Budget = 1000,
                    Revenue = 100000,
                    Popularity = 10,
                    TagLine = "test",
                    VoteAverage = 5,
                    VoteCount = 1000,
                    OriginalLanguage = "ua",
                    OriginalTitle = "Test",
                    Title = "Test Cool",
                    Overview = "Cool",
                    ReleaseDateTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    State = "Released"
                }));

            return Task.FromResult(result);
        }
    }
}
