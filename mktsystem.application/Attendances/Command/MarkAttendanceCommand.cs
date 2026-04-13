using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using mktsystem.domain.Entities;

namespace mktsystem.application.Attendances.Command
{
    public class MarkAttendanceCommand : IRequest<int>
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public AttendanceStatus Status { get; set; }

    }
}
