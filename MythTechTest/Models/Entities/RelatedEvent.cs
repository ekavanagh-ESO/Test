namespace MythTechTest.Models.Entities;

public class RelatedEvent
{
    public int Id { get; set; }
    public string SportsEventId { get; set; }
    public string RelatedEventId { get; set; }
    public string Type { get; set; }
    public string? TypeDetail { get; set; }
    public string? Depth { get; set; }
    public NavigationInfo? NavigationInfo { get; set; }
    public virtual SportsEvent SportsEvent { get; set; }
}