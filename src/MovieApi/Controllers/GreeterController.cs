using Microsoft.AspNetCore.Mvc;
using MovieApi.Request;
using MovieApi.Response;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreeterController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(HelloRequest request) => Ok(new HelloResponse { Message = "Hello " + request.Name });
    }
}
