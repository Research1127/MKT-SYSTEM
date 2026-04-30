using mktsystem.domain.Entities;


namespace mktsystem.domain.Repositories
{
    public interface IAttendanceRepository
    {
        Task<bool> Exists(int studentId, DateTime date);
        Task Add(Attendance attendance);

        Task AddRange(List<Attendance> attendances);

        Task MarkAttendanceAsync(int studentId, DateTime date, int classId, AttendanceStatus status);

        Task<List<Students>> GetStudentsAsync();
        Task<List<Classes>> GetClassesAsync();
        Task BulkInsertAsync(List<Attendance> data);
    }
}
