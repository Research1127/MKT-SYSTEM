using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mktsystem.application.Attendances.Command;
using mktsystem.application.Attendances.Command.BulkStudentAttendance;
using mktsystem.application.Attendances.Command.ExcelFileUploader;

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


    [HttpPost("excel")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadExcel([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is empty");

        if (!file.FileName.EndsWith(".xlsx"))
            return BadRequest("Only .xlsx files are allowed");

        var result = await mediator.Send(new UploadAttendanceCommand(file));
        return Ok(result);
    }
}

