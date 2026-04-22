using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mktsystem.application.Attendances.Command.BulkStudentAttendance.Dtos
{
    public class StudentsAttendanceDto
    {
        public int StudentId { get; set; }
        public string RequestedStatus { get; set; }
        public string FinalStatus { get; set; }
        public string Result {  get; set; }
        public string Message { get; set; }

    }
}
