using System;
using System.Security.Claims;

namespace mktsystem.domain.Entities
{
    public class Attendance
    {
        public int Id { get; set; }

        // ✅ Student attendance
        public int? StudentId { get; set; }
        public Students? Student { get; set; }

        // ✅ Teacher attendance (NOW uses User)
        public string? TeacherId { get; set; }
        public Users? Teacher { get; set; }

        // ✅ Class (only for students)
        public int? ClassId { get; set; }
        public Classes? Class { get; set; }

        public DateTime Date { get; set; }

        public AttendanceType Type { get; set; }

        public AttendanceStatus Status { get; set; }

        // ✅ Who marked (Admin / Teacher)
        public string MarkedBy { get; set; }
        public Users? MarkedByUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum AttendanceType
    {
        Student = 1,
        Teacher = 2
    }

    public enum AttendanceStatus
    {
        Present = 1,
        Absent = 2,
        Late = 3
    }
}