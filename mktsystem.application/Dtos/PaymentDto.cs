using mktsystem.domain.Entities;

namespace mktsystem.application.Dtos;

public class PaymentDto
{
    public string Month { get; set; } = string.Empty; // change to string
    public int Year { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
}