namespace MythTechTest.Models.DTOs;

public class RelatedEventDto
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string TypeDetail { get; set; }
    public string Depth { get; set; }
    public NavigationInfoDto NavigationInfo { get; set; }
}