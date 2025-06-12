namespace MythTechTest.Models.DTOs;

public class SportsEventDto
{
    public string Id { get; set; }
    public string Description { get; set; }
    public int Type { get; set; }
    public DateTime StartDateLocal { get; set; }
    public DateTime ScheduledStartTimeUtc { get; set; }
    public DateTime? EndTime { get; set; }
    public int Status { get; set; }
    public int? Attendance { get; set; }
    public string SportId { get; set; }
    public string VenueId { get; set; }
    public string HomeParticipantId { get; set; }
    public string AwayParticipantId { get; set; }
    public Dictionary<string, string> State { get; set; }
    public List<string> SportsOrganizationIds { get; set; }
    public List<string> ParentSportsEventIds { get; set; }
    public List<RelatedEventDto> RelatedSportsEvents { get; set; }
    public EventMetaDto Meta { get; set; }
}