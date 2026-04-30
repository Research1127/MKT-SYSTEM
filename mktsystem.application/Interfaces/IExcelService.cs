using Microsoft.AspNetCore.Http;
using mktsystem.application.Attendances.Command.ExcelFileUploader.Dtos;

namespace mktsystem.application.Interfaces
{
    public interface IExcelService
    {
        Task<List<ExcelAttendanceDto>> ParseAsync(IFormFile file);
    }

}
