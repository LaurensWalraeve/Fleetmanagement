using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.API.Controllers;

[ApiController]
public class PingController : ControllerBase
{
    [HttpGet]
    [Route("api/ping")]
    public IActionResult Ping()
    {
        return Ok("pong");
    }
}