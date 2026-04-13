using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using mktsystem.domain.Repositories;
using mktsystem.domain.Entities;
using Microsoft.AspNetCore.Http;

namespace mktsystem.application.Attendances.Command
{
    public class MarkAttendanceCommandHandler(ILogger<MarkAttendanceCommandHandler> logger,
        IAttendanceRepository attendanceRepository,
        IHttpContextAccessor httpContextAccessor) : IRequestHandler<MarkAttendanceCommand, int>
    {
        public async Task<int> Handle(MarkAttendanceCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Marking Attendance");

            var today = DateTime.UtcNow.Date;

            // (Optional but recommended) duplicate check
            var exists = await attendanceRepository.Exists(request.StudentId, today);
            if (exists)
                throw new Exception("Attendance already marked");

            if (request.StudentId <= 0 || request.ClassId <= 0)
                throw new Exception("Invalid StudentId or ClassId");

            var userId = httpContextAccessor.HttpContext?.User?
                .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;



            var attendance = new Attendance
            {
                StudentId = request.StudentId,
                ClassId = request.ClassId,
                Status = request.Status,
                Date = today,
                MarkedBy = userId,
                TeacherId = userId,

            };

            await attendanceRepository.Add(attendance);

            return attendance.Id;
        }
    }
}
