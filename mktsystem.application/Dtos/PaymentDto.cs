using mktsystem.domain.Entities;

namespace mktsystem.application.Dtos;

public class PaymentDto
{
    public string Month { get; set; } = string.Empty; // change to string
    public int Year { get; set; }
    
    public decimal PaidAmount { get; set; }
    
    public string PaymentStatus { get; set; } = string.Empty;
    
    public decimal DueAmount { get; set; }
    public decimal OutstandingAmount { get; set; }
    
}