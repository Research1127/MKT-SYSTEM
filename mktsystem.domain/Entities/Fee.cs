namespace mktsystem.domain.Entities;

public class Fee
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal MinIncome { get; set; }
    public decimal MaxIncome { get; set; } 
    public decimal FeeAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}