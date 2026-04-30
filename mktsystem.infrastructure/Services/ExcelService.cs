using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using mktsystem.application.Attendances.Command.ExcelFileUploader.Dtos;
using mktsystem.application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mktsystem.infrastructure.Services
{
    public class ExcelService : IExcelService
    {
        public async Task<List<ExcelAttendanceDto>> ParseAsync(IFormFile file)
        {
            var list = new List<ExcelAttendanceDto>();

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1);

            var rows = worksheet.RangeUsed().RowsUsed().Skip(1);

            foreach (var row in rows)
            {


                var ExcelList = new ExcelAttendanceDto()
                {
                    Name = row.Cell(1).GetString(),
                    IcNumber = row.Cell(2).GetString(),
                    ClassName = row.Cell(3).GetString(),
                    Status = row.Cell(4).GetString(),
                    Date = row.Cell(5).GetString()

                };

                list.Add(ExcelList);
            }

            return list;
        }
    }
}
