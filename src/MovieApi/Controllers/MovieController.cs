using Microsoft.AspNetCore.Mvc;
using MovieApi.Services;
using MovieModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieClient _movieClient;

        public MovieController(IMovieClient movieClient)
        {
            _movieClient = movieClient;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(string id)
        {
            return Ok(await _movieClient.GetMovie(id));
        }

        [HttpPost("batch/get")]
        public async Task<IActionResult> GetBatchMovie(IEnumerable<string> ids)
        {
            return Ok(await _movieClient.GetBatchMovie(ids));
        }

        [HttpPost()]
        public async Task<IActionResult> SetMovie(Movie movie)
        {
            await _movieClient.SetMovie(movie);
            return Ok();
        }

        [HttpPost("batch/set")]
        public async Task<IActionResult> SetBatchMovie(IEnumerable<Movie> movies)
        {
            await _movieClient.SetBatchMovie(movies);
            return Ok();
        }

        [HttpGet("ping/{count}")]
        public async Task<IActionResult> PingRedis(int count)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await _movieClient.PingRedisAsync(count);

            stopwatch.Stop();

            return Ok(stopwatch.ElapsedMilliseconds);
        }

        [HttpGet("ping/pipeline/{count}")]
        public async Task<IActionResult> PingPipelineRedis(int count)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await _movieClient.PingRedisAsync(count);

            stopwatch.Stop();

            return Ok(stopwatch.ElapsedMilliseconds);
        }

        //[HttpGet]
        //public IEnumerable<Movie> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 50).Select(index => new Movie
        //    {
        //        Id = 1,
        //        ImdbId = "2",
        //        Budget = 1000,
        //        Revenue = 100000,
        //        Popularity = 10,
        //        TagLine = "test",
        //        VoteAverage = 5,
        //        VoteCount = 1000,
        //        OriginalLanguage = "ua",
        //        OriginalTitle = "Test",
        //        Title = "Test Cool",
        //        Overview = "Cool",
        //        ReleaseDateTime = DateTime.Now,
        //        State = "Released"
        //    })
        //    .ToArray();
        //}
    }
}
