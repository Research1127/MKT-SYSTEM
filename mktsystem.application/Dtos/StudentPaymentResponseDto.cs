namespace mktsystem.application.Dtos;

public class StudentPaymentResponseDto
{
    public string Name { get; set; } = string.Empty;
    public string IcNumber { get; set; } = string.Empty;
    public List<PaymentDto> Payments { get; set; } = new();
}