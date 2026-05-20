namespace apbd_2026_cw10;

public class GetPcComponentsDto
{
    public int Amount { get; set; }
    public List<GetComponentDto> Component { get; set; } = [];
}