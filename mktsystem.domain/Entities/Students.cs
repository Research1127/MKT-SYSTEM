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
    
    // Extra student info
    public string Address { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string School { get; set; } = string.Empty;
    public string FatherJob { get; set; } = string.Empty;
    public string MotherJob { get; set; } = string.Empty;
    public decimal FamilyIncome { get; set; }
    public int SiblingsCount  { get; set; }
    public List<Payments> Payments { get; set; } = new List<Payments>();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}