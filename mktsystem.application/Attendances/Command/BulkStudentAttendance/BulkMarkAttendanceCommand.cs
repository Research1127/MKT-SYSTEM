using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using mktsystem.application.Attendances.Command.BulkStudentAttendance.Dtos;

namespace mktsystem.application.Attendances.Command.BulkStudentAttendance
{
    public class BulkMarkAttendanceCommand : IRequest<BulkMarkAttendanceResponseDto>
    {
        public int ClassId { get; set; }
        public DateTime Date { get; set; }
        public List<StudentsAttendanceRequestDto> StudentsRequest { get; set; }
    }
}
