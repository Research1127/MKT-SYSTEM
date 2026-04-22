using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mktsystem.application.Attendances.Command.BulkStudentAttendance.Dtos
{
    public class BulkMarkAttendanceResponseDto
    {
        public string Status { get; set; }
        public int ClassId { get; set; }
        public DateTime Date {  get; set; }
        public SummaryDto Summary { get; set; }
        public List<StudentsAttendanceDto> StudentsAttendance { get; set; }

        

    }
}
