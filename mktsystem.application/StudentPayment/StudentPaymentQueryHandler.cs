using MediatR;
using Microsoft.Extensions.Logging;
using mktsystem.application.Dtos;
using mktsystem.domain.Repositories;

namespace mktsystem.application.StudentPayment;

public class StudentPaymentQueryHandler(
    ILogger<StudentPaymentQueryHandler> logger,
    IStudentRepository studentRepository
) : IRequestHandler<StudentPaymentQuery, StudentPaymentResponseDto>
{
    public async Task<StudentPaymentResponseDto> Handle(StudentPaymentQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving payments for IC: {IcNumber}", request.IcNumber);

        // Get student + payments from repository
        var student = await studentRepository.GetStudentWithPaymentsByIc(request.IcNumber, cancellationToken);

        if (student == null)
        {
            logger.LogWarning("Student not found: {IcNumber}", request.IcNumber);
            return null; // Controller can return 404
        }

        // Get monthly fee from the class
        var monthlyFee = student.Class.MonthlyFee;

        decimal totalDue = 0;
        decimal totalOutstanding = 0;

        // Build DTO list manually with calculated fields
        var paymentDtos = new List<PaymentDto>();
        foreach (var payment in student.Payments)
        {
            var dueAmount = monthlyFee;
            var outstandingAmount = dueAmount - payment.PaidAmount;

            totalDue += dueAmount;
            totalOutstanding += outstandingAmount;

            paymentDtos.Add(new PaymentDto
            {
                Month = payment.Month.ToString(),
                Year = payment.Year,
                PaymentStatus = payment.PaymentStatus.ToString(),
                PaidAmount = payment.PaidAmount,
                DueAmount = dueAmount,
                OutstandingAmount = outstandingAmount
            });
        }

        // Build final response
        var response = new StudentPaymentResponseDto
        {
            Name = student.Name,
            IcNumber = student.IcNumber,
            Payments = paymentDtos,
            TotalDue = totalDue,
            TotalOutstanding = totalOutstanding
        };

        return response;
    }
}