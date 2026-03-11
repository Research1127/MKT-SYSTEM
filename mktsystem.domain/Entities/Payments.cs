namespace mktsystem.domain.Entities;

public class Payments
{
    public int Id { get; set; }
    public Students? Student { get; set; }
    public int StudentId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public enum PaymentStatus
{
    Paid,
    Unpaid,
}