using MediatR;
using mktsystem.application.Dtos;

namespace mktsystem.application.StudentPayment;

public record StudentPaymentQuery(string IcNumber) : IRequest<StudentPaymentResponseDto>;