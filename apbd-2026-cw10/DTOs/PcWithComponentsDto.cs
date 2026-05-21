

namespace apbd_2026_cw10;

public class PcWithComponentsDto : GetPcsDto
{
    public List<GetPcComponentsDto> PcComponents { get; set; } = [];
}