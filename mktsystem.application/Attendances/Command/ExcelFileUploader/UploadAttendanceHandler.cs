using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using mktsystem.application.Attendances.Command.ExcelFileUploader.Dtos;
using mktsystem.application.Interfaces;
using mktsystem.domain.Entities;
using mktsystem.domain.Repositories;
using System.Linq;
using System.Security.Claims;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace mktsystem.application.Attendances.Command.ExcelFileUploader
{
    public class UploadAttendanceHandler(ILogger<UploadAttendanceHandler> logger,
        IAttendanceRepository attendanceRepository,
        IExcelService excelServices,
        IHttpContextAccessor httpContextAccessor) : IRequestHandler<UploadAttendanceCommand, UploadResultDto>
    {
        public async Task<UploadResultDto> Handle(UploadAttendanceCommand request, CancellationToken cancellationToken)
        {
            var result = new UploadResultDto();

            // Parse Data

            var excelData = await excelServices.ParseAsync(request.File);

            // Load Reference Data

            var students = await attendanceRepository.GetStudentsAsync();

            var classes = await attendanceRepository.GetClassesAsync();

            // Setup Dictionary
            // Group by to prevent crash
            var studentDict = students
                .Where(s => !string.IsNullOrEmpty(s.IcNumber))
                .GroupBy(s => s.IcNumber.Trim().ToLower())
                .ToDictionary(g => g.Key, g => g.First());

            var classDict = classes
                .Where(c => !string.IsNullOrEmpty(c.Name))
                .GroupBy(c => c.Name.Trim().ToLower())
                .ToDictionary(g => g.Key, g => g.First());

            // Looping through the excel file

            var validList = new List<Attendance>();

            int rowNumber = 1;

            // Detect User Authentication
            var userId = httpContextAccessor.HttpContext?.User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;


            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("UserId not found or invalid token");
            }

            logger.LogInformation("UserId: {UserId}", userId);


            DateTime parsedDate;

            foreach ( var row in excelData)
            {
                rowNumber ++;

                var ic = row.IcNumber?.Trim().ToLower();
                var className = row.ClassName?.Trim().ToLower();



                if (!studentDict.TryGetValue(ic, out var student))
                {
                    result.Errors.Add($"Row Number {rowNumber}: Student Not Found");
                    result.FailedCount++;
                    continue;
             

                }

                if (!classDict.TryGetValue(className, out var cls))
                {
                    result.Errors.Add($"Row Number {rowNumber}: Class Not Found");
                    result.FailedCount++;
                    continue;


                }

                if (string.IsNullOrEmpty(row.Status))
                {
                    result.Errors.Add($"Row {rowNumber}: Status is empty");
                    result.FailedCount++;
                    continue;
                }

                if (!Enum.TryParse<AttendanceStatus>(row.Status, true, out var finalStatus))
                {
                    result.Errors.Add($"Row {rowNumber}: Invalid Status");
                    result.FailedCount++;
                    continue;
                }

                if (!DateTime.TryParseExact(row.Date?.Trim(),"yyyy-MM-dd", CultureInfo.InvariantCulture,DateTimeStyles.None,out parsedDate))
                {
                    result.Errors.Add($"Row {rowNumber}: Invalid date format. Use yyyy-MM-dd");
                    result.FailedCount++;
                    continue;
                }
                parsedDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);


                validList.Add(new Attendance
                    {
                        StudentId = student.Id,
                        ClassId = cls.Id,
                        Status = finalStatus,
                        Date = parsedDate,
                        MarkedBy = userId,
                        TeacherId = userId
                        
                    });

                result.SuccessCount++;



            }

            if (!validList.Any())
            {
                logger.LogWarning("No valid data to insert");
                return result;
            }

            await attendanceRepository.BulkInsertAsync(validList);
            return result;


        }
    }
}
