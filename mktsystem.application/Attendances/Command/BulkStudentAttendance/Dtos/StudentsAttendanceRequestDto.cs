using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mktsystem.application.Attendances.Command.BulkStudentAttendance.Dtos
{
    public class StudentsAttendanceRequestDto
    {
        public int StudentId { get; set; }
        public string Status { get; set; }
    }
}
