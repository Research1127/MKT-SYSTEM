using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mktsystem.application.Attendances.Command;

namespace mktsystem.Controllers;

[ApiController]
[Route("api/attendance")]
[Authorize]
public class AttendanceController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> MarkAttendanceStudent([FromBody] MarkAttendanceCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);

    }
}

