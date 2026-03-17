namespace mktsystem.domain.Entities;

public class Classes
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal MonthlyFee { get; set; }
    
    public List<Students> Students { get; set; } = new List<Students>();
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

}