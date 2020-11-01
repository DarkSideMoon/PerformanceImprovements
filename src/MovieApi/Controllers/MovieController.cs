using Microsoft.AspNetCore.Mvc;
using MovieModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 250).Select(index => new Movie
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
    }
}
