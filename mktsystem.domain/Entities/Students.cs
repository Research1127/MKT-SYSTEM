using System.ComponentModel.DataAnnotations.Schema;

namespace mktsystem.domain.Entities;

public class Students
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IcNumber { get; set; }
    
    // Add ClassId
    public int ClassId { get; set; } // FK to Classes table
    public Classes Class { get; set; } // optional navigation property

    public int Year { get; set; }
    public List<Payments> Payments { get; set; } = new List<Payments>();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}