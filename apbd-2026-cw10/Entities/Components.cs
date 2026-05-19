using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd_2026_cw10.Entities;

[Table("Components")]
public class Components
{
    [Key]
    [Column(TypeName = "char(10)")]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;
    
    [MaxLength(300)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(Int32.MaxValue)]
    public string Description { get; set; } =  string.Empty;
    
    public int ComponentManufacturesId {get; set;}
    [ForeignKey(nameof(ComponentManufacturesId))]
    public ComponentManufacturers ComponentManufacturers { get; set; } = null!;
    
    public int ComponentTypesId {get; set;}
    [ForeignKey(nameof(ComponentTypesId))]
    public ComponentTypes ComponentTypes { get; set; } = null!;
    
    public ICollection<PcComponents> PcComponents { get; set; } = [];
}