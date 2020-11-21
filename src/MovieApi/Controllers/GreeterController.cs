using Microsoft.AspNetCore.Mvc;
using MovieModel.Rest;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreeterController : ControllerBase
    {
        [HttpGet("{name}")]
        public IActionResult Get(string name) => Ok(new HelloResponse { Message = "Hello " + name });
    }
}
