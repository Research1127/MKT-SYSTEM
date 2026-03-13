using MediatR;
using Microsoft.AspNetCore.Mvc;
using mktsystem.application.StudentPayment;

namespace mktsystem.Controllers;
[ApiController]
[Route("api/payments")]
public class PaymentsController(IMediator mediator) : ControllerBase
{
    [HttpGet("{icNumber}")]
    public async Task<IActionResult> GetStudentPaymentsByIc(string icNumber)
    {
        var result = await mediator.Send(new StudentPaymentQuery(icNumber));
        if (result == null)
            return NotFound(new { message = $"Student with IC {icNumber} not found" });
        return Ok(result);
    }
}