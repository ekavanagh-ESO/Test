namespace MythTechTest.Models.Entities;

public class EventState
{
    public int Id { get; set; }
    public string SportsEventId { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public virtual SportsEvent SportsEvent { get; set; }
}