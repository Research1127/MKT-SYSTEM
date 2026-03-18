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

        var student = await studentRepository.GetStudentWithPaymentsByIc(request.IcNumber, cancellationToken);

        if (student == null)
        {
            logger.LogWarning("Student not found: {IcNumber}", request.IcNumber);
            return null;
        }

        var monthlyFee = student.Class.MonthlyFee;

        decimal totalDue = 0;
        decimal totalOutstanding = 0;
        decimal totalPaid = 0;

        // Group payments by Month + Year
        var groupedPayments = student.Payments
            .GroupBy(p => new { p.Month, p.Year })
            .Select(g =>
            {
                var totalPaidInMonth = g.Sum(p => p.PaidAmount);
                var dueAmount = monthlyFee;
                var outstandingAmount = dueAmount - totalPaidInMonth;

                // Monthly Status
                string monthlyStatus;
                if (totalPaidInMonth == 0)
                    monthlyStatus = "Unpaid";
                else if (totalPaidInMonth < dueAmount)
                    monthlyStatus = "Partially";
                else
                    monthlyStatus = "Paid";

                // Increment totals
                totalDue += dueAmount;
                totalPaid += totalPaidInMonth;
                totalOutstanding += outstandingAmount;

                return new PaymentDto
                {
                    Month = g.Key.Month.ToString(),
                    Year = g.Key.Year,
                    PaidAmount = totalPaidInMonth,
                    DueAmount = dueAmount,
                    OutstandingAmount = outstandingAmount,
                    PaymentStatus = monthlyStatus
                };
            })
            .OrderBy(p => p.Year)
            .ThenBy(p => p.Month)
            .ToList();

        // Overall Status
        string overallStatus;
        if (totalPaid == 0)
            overallStatus = "No fees have been paid yet";
        else if (totalPaid < totalDue)
            overallStatus = "Partial fees paid, some outstanding";
        else
            overallStatus = "All fees are paid";

        // Build response
        return new StudentPaymentResponseDto
        {
            Name = student.Name,
            IcNumber = student.IcNumber,
            Payments = groupedPayments,
            TotalPaid = totalPaid,
            TotalDue = totalDue,
            TotalOutstanding = totalOutstanding,
            OverallStatus = overallStatus
        };
    }
}