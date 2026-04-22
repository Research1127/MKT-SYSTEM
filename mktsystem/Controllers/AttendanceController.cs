using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mktsystem.application.Attendances.Command;
using mktsystem.application.Attendances.Command.BulkStudentAttendance;

namespace mktsystem.Controllers;

[ApiController]
[Route("api/attendance")]
[Authorize]
public class AttendanceController(IMediator mediator) : ControllerBase
{
    [HttpPost("single")]
    public async Task<IActionResult> MarkAttendanceStudent([FromBody] MarkAttendanceCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);

    }

    [HttpPost("bulk")]
    public async Task<IActionResult> BulkMarkAttendanceStudent([FromBody] BulkMarkAttendanceCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}

