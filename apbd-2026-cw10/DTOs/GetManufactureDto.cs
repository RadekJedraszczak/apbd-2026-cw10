namespace apbd_2026_cw10;

public class GetManufactureDto
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = string.Empty;
    public string FullName { get; set; } =  string.Empty;
    public DateTime FoundationDate { get; set; }
}