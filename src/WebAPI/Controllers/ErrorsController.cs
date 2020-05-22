using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [Route("Error")]
        [HttpGet]
        public IActionResult HandleError()
        {
            var data = new { Error = new { Message = "Internal Server Error" } };
            return StatusCode(StatusCodes.Status500InternalServerError, data);
        }
    }
}
