using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetAddressManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Connect to server at " + DateTime.Now);
        }
    }
}
