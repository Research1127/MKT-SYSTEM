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

        public async Task AddRange(List<Attendance> attendances)
        {
            await dbContext.Attendances.AddRangeAsync(attendances);
            await dbContext.SaveChangesAsync();
        }

        // Marking Attendance
        public async Task MarkAttendanceAsync(int studentId, DateTime date, int classId, AttendanceStatus status)
        {
            var attendance = new Attendance
            {
                StudentId = studentId,
                Date = date,
                ClassId = classId,
                Status = status

            };

            dbContext.Attendances.Add(attendance);
            await dbContext.SaveChangesAsync();
        }

        // Call List of Students
        public async Task<List<Students>> GetStudentsAsync() 
        {
            var students = await dbContext.Students.ToListAsync();
            return students;
        }

        // Call List of Classes
        public async Task<List<Classes>> GetClassesAsync()
        { 
            var classes = await dbContext.Classes.ToListAsync();
            return classes;
        }

        // Excel Bulk Insert
        public async Task BulkInsertAsync(List<Attendance> data)
        {
            await dbContext.Attendances.AddRangeAsync(data);
            await dbContext.SaveChangesAsync();
        }
    }
}
