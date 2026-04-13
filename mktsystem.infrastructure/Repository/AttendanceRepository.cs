using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mktsystem.domain.Entities;
using mktsystem.domain.Repositories;
using mktsystem.infrastructure.Persistence;

namespace mktsystem.infrastructure.Repository
{
    public class AttendanceRepository(MktSystemDbContext dbContext) : IAttendanceRepository
    {
        public async Task<bool> Exists(int studentId, DateTime date)
        {
            return await dbContext.Attendances.AnyAsync(x => x.StudentId == studentId && x.Date == date);
        }

        public async Task Add(Attendance attendance)
        {
            await dbContext.Attendances.AddAsync(attendance);
            await dbContext.SaveChangesAsync();
        }
    }
}
