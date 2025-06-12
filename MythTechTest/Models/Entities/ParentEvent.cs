namespace MythTechTest.Models.Entities;

public class ParentEvent
{
    public int Id { get; set; }
    public string SportsEventId { get; set; }
    public string ParentEventId { get; set; }
    public virtual SportsEvent SportsEvent { get; set; }
}