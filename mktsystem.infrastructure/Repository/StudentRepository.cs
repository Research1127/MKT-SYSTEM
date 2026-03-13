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
            .FirstOrDefaultAsync(s => s.IcNumber == icNumber, cancellationToken);
    }
}