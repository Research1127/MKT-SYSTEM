using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Logging;
using mktsystem.application.Attendances.Command.BulkStudentAttendance.Dtos;
using mktsystem.domain.Entities;
using mktsystem.domain.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace mktsystem.application.Attendances.Command.BulkStudentAttendance
{
    public class BulkMarkAttendanceCommandHandler(
        ILogger<BulkMarkAttendanceCommandHandler> logger,
        IAttendanceRepository attendanceRepository,
        IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<BulkMarkAttendanceCommand, BulkMarkAttendanceResponseDto>
    {
        public async Task<BulkMarkAttendanceResponseDto> Handle(
            BulkMarkAttendanceCommand request,
            CancellationToken cancellationToken)
        {
            logger.LogInformation("Marking Attendances");

            var response = new BulkMarkAttendanceResponseDto()
            {
                ClassId = request.ClassId,
                Date = request.Date.Date,
                Summary = new SummaryDto(),
                StudentsAttendance = new List<StudentsAttendanceDto>()
                
            };

            // Detect User Authentication
            var userId = httpContextAccessor.HttpContext?.User?
                        .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var attendances = new List<Attendance>();

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not authenticated");
            }

            int present = 0, absent = 0, failed = 0;

            foreach (var student in request.StudentsRequest)
            {
                var result = new StudentsAttendanceDto()
                {
                    StudentId = student.StudentId,
                    RequestedStatus = student.Status,
                };

                // ✅ ONLY use enum validation
                if (!Enum.TryParse<AttendanceStatus>(
                    student.Status,
                    true,
                    out var finalStatus))
                {
                    result.Result = "Failed";
                    result.Message = "Invalid status";
                    failed++;

                    response.StudentsAttendance.Add(result);
                    continue;
                }

                // ⚠️ IMPORTANT: Adjust based on your Exists meaning
                var attendanceExists = await attendanceRepository.Exists(
                    student.StudentId,
                    request.Date.Date
                );

                if (attendanceExists)
                {
                    result.Result = "Failed";
                    result.Message = "Attendance already recorded"; // ✅ FIXED
                    failed++;

                    response.StudentsAttendance.Add(result);
                    continue;
                }

                try
                {
                    

                    var attendance = new Attendance
                    {
                        StudentId = student.StudentId,
                        Date = request.Date.Date,
                        ClassId = request.ClassId,
                        Status = finalStatus,
                        MarkedBy = userId,
                        TeacherId = userId,

                    };

                    attendances.Add(attendance);



                    result.Result = "Success";
                    result.FinalStatus = finalStatus.ToString();
                    result.Message = "Recorded";

                    if (finalStatus == AttendanceStatus.Present) present++;
                    else if (finalStatus == AttendanceStatus.Absent) absent++;
                }
                catch (Exception ex)
                {
                    result.Result = "Failed";
                    result.Message = ex.Message;
                    failed++;
                }

                response.StudentsAttendance.Add(result);
            }
            await attendanceRepository.AddRange(attendances);

            // ✅ FIXED
            response.Summary.Total = request.StudentsRequest.Count;
            response.Summary.Present = present;
            response.Summary.Absent = absent;
            response.Summary.Failed = failed;

            response.Status = failed > 0 ? "partial_success" : "success";

            return response;
        }
    }
}