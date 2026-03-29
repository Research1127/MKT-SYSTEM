using Microsoft.AspNetCore.Mvc;

namespace mktsystem.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{

    // GET request
    [HttpGet]
    public IActionResult Get() => Ok("Healthy");

    // HEAD request (UptimeRobot sometimes uses this)
    [HttpHead]
    public IActionResult Head() => Ok();
}