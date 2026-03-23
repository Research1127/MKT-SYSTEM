using mktsystem.domain.Entities;

namespace mktsystem.domain.Repositories;

public interface IStudentRepository
{
    Task<Students?> GetStudentWithPaymentsByIc(string icNumber, CancellationToken cancellationToken);

    Task<decimal> GetFeeAmount(decimal familyIncome, CancellationToken cancellationToken);

}