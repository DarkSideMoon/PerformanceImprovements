using Microsoft.AspNetCore.Mvc;
using MovieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerformanceController : ControllerBase
    {
        [HttpGet("movies")]
        public IEnumerable<Movie> Get()
        {
            var rng = new Random();
            return Enumerable.Range(20, 20).Select(index => new Movie
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
                ReleaseDateTime = DateTime.Now,
                State = "Released"
            })
            .ToArray();
        }

        [HttpGet("movie")]
        public Movie GetMovie()
        {
            return new Movie
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
                ReleaseDateTime = DateTime.Now,
                State = "Released"
            };
        }
    }
}
