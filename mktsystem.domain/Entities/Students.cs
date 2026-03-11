namespace mktsystem.domain.Entities;

public class Students
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IC_Number { get; set; }
    public string Class { get; set; }
    public int Year { get; set; }
    public List<Payments> Payments { get; set; } = new List<Payments>();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}