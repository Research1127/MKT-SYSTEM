using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using mktsystem.application.Dtos;
using mktsystem.application.StudentPayment;
using mktsystem.domain.Repositories;

namespace mktsystem.application.StudentPayment;

public class StudentPaymentQueryHandler(
    ILogger<StudentPaymentQueryHandler> logger,
    IMapper mapper,
    IStudentRepository studentRepository
) : IRequestHandler<StudentPaymentQuery, StudentPaymentResponseDto>
{
    public async Task<StudentPaymentResponseDto> Handle(StudentPaymentQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving payments for IC: {IcNumber}", request.IcNumber);

        var student = await studentRepository.GetStudentWithPaymentsByIc(request.IcNumber, cancellationToken);

        if (student == null)
        {
            logger.LogWarning("Student not found: {IcNumber}", request.IcNumber);
            return null; // Controller can handle NotFound
        }

        var response = new StudentPaymentResponseDto
        {
            Name = student.Name,
            IcNumber = student.IcNumber,
            Payments = mapper.Map<List<PaymentDto>>(student.Payments)
        };

        return response;
    }
}