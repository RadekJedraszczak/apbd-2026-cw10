using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd_2026_cw10.Entities;

[Table("ComponentTypes")]
public class ComponentTypes
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(30)]
    public string Abbreviation { get; set; } = string.Empty;
    
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Components> Components { get; set; } = [];
}