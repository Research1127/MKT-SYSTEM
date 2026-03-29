using Microsoft.AspNetCore.Mvc;

namespace mktsystem.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    // Simple GET endpoint for uptime monitoring
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Healthy");
    }
}