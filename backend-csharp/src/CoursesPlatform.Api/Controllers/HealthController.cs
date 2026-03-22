using Microsoft.AspNetCore.Mvc;

namespace CoursesPlatform.Api.Controllers;

[ApiController]
public sealed class HealthController : ControllerBase
{
    [HttpGet("/health")]
    public IActionResult Get()
    {
        return Ok(new { status = "ok" });
    }
}
