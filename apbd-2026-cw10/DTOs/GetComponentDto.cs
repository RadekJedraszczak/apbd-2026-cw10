namespace apbd_2026_cw10;

public class GetComponentDto
{
   public string Code {get; set;}
   public string Name {get; set;} =  string.Empty;
   public string Description {get; set;} = string.Empty;
   public List<GetManufactureDto> Manufacturer {get; set;} = [];
   public List<GetTypeDto> Type {get; set;} = [];
}