using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mktsystem.application.Attendances.Command.ExcelFileUploader.Dtos
{
    public class AttendanceDto
    {
        public int StudentId  { get; set; }
        public int ClassId { get; set; }
        public string Status { get; set; }
    }
}
