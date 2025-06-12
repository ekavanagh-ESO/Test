namespace MythTechTest.Models.Entities;

public class SportsOrganization
{
    public int Id { get; set; }
    public string SportsEventId { get; set; }
    public string OrganizationId { get; set; }
    public virtual SportsEvent SportsEvent { get; set; } 
}