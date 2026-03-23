using Microsoft.EntityFrameworkCore;
using mktsystem.domain.Entities;
using mktsystem.domain.Repositories;
using mktsystem.infrastructure.Persistence;

namespace mktsystem.infrastructure.Repository;

public class StudentRepository(MktSystemDbContext dbContext) : IStudentRepository
{
    public async Task<Students?> GetStudentWithPaymentsByIc(string icNumber, CancellationToken cancellationToken)
    {
        return await dbContext.Students
            .Include(s => s.Payments)
            .Include(s => s.Class)
            .FirstOrDefaultAsync(s => s.IcNumber == icNumber, cancellationToken);
    }

    public async Task<decimal> GetFeeAmount(decimal familyIncome, CancellationToken cancellationToken)
    {
        var fee = await dbContext.Fees
            .FirstOrDefaultAsync(f => familyIncome >= f.MinIncome &&
                                      (f.MaxIncome == null || familyIncome <= f.MaxIncome),
                cancellationToken);

        return fee?.FeeAmount ?? 0;
    }
}