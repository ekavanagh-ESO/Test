using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MythTechTest.Models.Entities
{
    public class SportsEvent
    {
        [Key]
        public string Id { get; set; }
        public string? Description { get; set; }
        public int Type { get; set; }
        public DateTime StartDateLocal { get; set; }
        public DateTime ScheduledStartTimeUtc { get; set; }
        public DateTime? EndTime { get; set; }
        public int Status { get; set; }
        public int? Attendance { get; set; }
        public string SportId { get; set; }
        public string? VenueId { get; set; }
        public string DirectParentSportsEventId { get; set; }
        
        public string? HomeParticipantId { get; set; }
        public string? AwayParticipantId { get; set; }
        public int ParticipantType { get; set; }

        public virtual ICollection<EventState> States { get; set; }
        public virtual ICollection<SportsOrganization> SportsOrganizations { get; set; }
        public virtual ICollection<ParentEvent> ParentEvents { get; set; }
        public virtual ICollection<RelatedEvent> RelatedEvents { get; set; }
        public virtual EventMeta Meta { get; set; }
    }
}