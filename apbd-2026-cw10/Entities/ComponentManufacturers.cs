using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd_2026_cw10.Entities;

[Table("ComponentManufacturers")]
public class ComponentManufacturers
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(30)]
    public string Abbreviation { get; set; } = string.Empty;
    
    [MaxLength(300)]
    public string FullName { get; set; } = string.Empty;
    
    [Column(TypeName = "date")]
    public DateTime FoundationDate { get; set; }
    
    public ICollection<Components> Components { get; set; } = [];
}