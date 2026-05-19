using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd_2026_cw10.Entities;

[Table("PCComponents")]
[PrimaryKey(nameof(PcId), nameof(ComponentCode))]
public class PcComponents
{   
    
    public int PcId { get; set; }
    [ForeignKey(nameof(PcId))]
    public PCs Pc { get; set; } = null!;
    
    public string ComponentCode { get; set; }  = string.Empty;
    [ForeignKey(nameof(ComponentCode))]
    public Components Component { get; set; } = null!;
    
    public int Amount { get; set; }
}