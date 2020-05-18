using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        [Route("Health")]
        [HttpGet]
        public IActionResult CheckHealth()
        {
            return this.Ok("The service is available.");
        }
    }
}