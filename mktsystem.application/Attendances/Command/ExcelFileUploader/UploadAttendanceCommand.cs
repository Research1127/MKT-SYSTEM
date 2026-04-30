using MediatR;
using Microsoft.AspNetCore.Http;
using mktsystem.application.Attendances.Command.ExcelFileUploader.Dtos;

namespace mktsystem.application.Attendances.Command.ExcelFileUploader
{
    public class UploadAttendanceCommand(IFormFile file) : IRequest<UploadResultDto>
    {
        public IFormFile File { get; } = file;
    }
}
