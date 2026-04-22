using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mktsystem.domain.Entities;

namespace mktsystem.domain.Repositories
{
    public interface IAttendanceRepository
    {
        Task<bool> Exists(int studentId, DateTime date);
        Task Add(Attendance attendance);

        Task AddRange(List<Attendance> attendances);

        Task MarkAttendanceAsync(int studentId, DateTime date, int classId, AttendanceStatus status);
    }
}
