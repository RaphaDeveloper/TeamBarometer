using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            return Created("", null);
        }
    }
}