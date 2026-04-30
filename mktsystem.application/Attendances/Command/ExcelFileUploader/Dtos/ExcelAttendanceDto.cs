using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mktsystem.application.Attendances.Command.ExcelFileUploader.Dtos
{
    public class ExcelAttendanceDto
    {
        public string Name { get; set; }
        public string IcNumber { get; set; }
        public string ClassName { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }


    }
}
