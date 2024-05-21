using Microsoft.AspNetCore.Mvc;

namespace OnCallShift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok("Test Controller");
        }
    }
}
