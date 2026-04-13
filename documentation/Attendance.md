# Attendance Setup

## Controller Creation

### File: AttendanceController.cs

Have 2 endpoints:
```text
POST /api/attendance/teacher
POST /api/attendance/student
```

---

## Entities Creation

### File: Attendance.cs
- Both Student And Teacher. With Type field

```csharp
public class Attendance
{
    public int Id { get; set; }

    public int? StudentId { get; set; }   // For student attendance
    public int? TeacherId { get; set; }   // For teacher attendance

    public int? ClassId { get; set; }     // Only for students

    public DateTime Date { get; set; }
    
    public AttendanceType Type { get; set; }
    
    public AttendanceStatus Status { get; set; }

    public int MarkedBy { get; set; }     // Who marked this
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
```

## DbContext Setup 

Adding unique constraint
```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Attendance>()
        .HasIndex(a => new { a.StudentId, a.ClassId, a.Date })
        .IsUnique();

    modelBuilder.Entity<Attendance>()
        .HasIndex(a => new { a.TeacherId, a.Date })
        .IsUnique();
}
```


---
## Migration

Setup migration and database update

---
## Setup IAttendanceRepository And AttendanceRepository

### File: IAttendanceRepository.cs

### File: AttendanceRepository.cs

---

## Setup Business Logic in Application Layer (Service Layer)

### File: Attendance/MarkingAttendanceDto.cs
### File: Attendance/MarkingStudentAttendanceCommand.cs
### File: Attendance/MarkingStudentAttendanceCommandHandler.cs
### File: Attendance/MarkingTeacherAttendanceCommand.cs
### File: Attendance/MarkingTeacherAttendanceCommandHandler.cs

Add Validation in Handler

Inside your handler:

TeacherHandler:

```csharp
if (request.TeacherId == null)
    throw new Exception("TeacherId is required");

```
StudentHandler:

```csharp
if (request.ClassId == null)
    throw new Exception("ClassId is required");

if (request.Attendance == null || !request.Attendance.Any())
    throw new Exception("Attendance list cannot be empty");
```

Notes

🧩 How It Works

👨‍🏫 Teacher Attendance (Salary Purpose)

Scenario:

Teacher logs in and marks their own attendance

Data example:

```json
{
  "TeacherId": 5,
  "Date": "2026-04-06",
  "Status": 1,
  "MarkedBy": 5
}

```

Notes:
•	❌ No StudentId
•	❌ No ClassId
•	✅ Only TeacherId

👨‍🎓 Student Attendance

Scenario:

Teacher marks attendance for class

Data example:

```json
{
  "ClassId": 2,
  "Date": "2026-04-06",
  "MarkedBy": 5,
  "Attendance": [
    { "StudentId": 10, "Status": 1 },
    { "StudentId": 11, "Status": 2 }
  ]
}
```

🔐 Important Rules (VERY IMPORTANT)

You must enforce this logic:

If Type = Teacher
•	TeacherId ✅ required
•	StudentId ❌ must be null
•	ClassId ❌ must be null

---

If Type = Student
•	StudentId ✅ required
•	ClassId ✅ required
•	TeacherId ❌ optional or null

---

👉 You can enforce this in:
•	Service layer (recommended)
•	OR database constraints (advanced)


