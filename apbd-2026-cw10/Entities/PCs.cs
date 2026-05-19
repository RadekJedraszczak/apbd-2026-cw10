using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd_2026_cw10.Entities;

[Table("PCs")]
public class PCs
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Column(TypeName = "float(5)")]
    public float Weight { get; set; }
    
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }

    public ICollection<PcComponents> PcComponents { get; set; } = [];
}