

namespace apbd_2026_cw10;

public class PcWithComponentsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
    public List<GetPcComponentsDto> PcComponents { get; set; } = [];
}