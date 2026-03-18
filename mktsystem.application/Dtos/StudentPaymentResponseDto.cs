namespace mktsystem.application.Dtos;

public class StudentPaymentResponseDto
{
    public string Name { get; set; } = string.Empty;
    public string IcNumber { get; set; } = string.Empty;
    public List<PaymentDto> Payments { get; set; } = new();
    
    public decimal TotalPaid  { get; set; }
    
    public decimal TotalDue { get; set; }
    public decimal TotalOutstanding { get; set; }
    
    public string OverallStatus { get; set; } = string.Empty;
}