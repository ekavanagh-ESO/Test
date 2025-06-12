using System.ComponentModel.DataAnnotations;

namespace MythTechTest.Models.Entities;

public class EventMeta
{
    [Key]
    public string SportsEventId { get; set; }
    public long UpdateId { get; set; }
    public string UpdateAction { get; set; }
    public DateTime UpdateDate { get; set; }
    public string Language { get; set; }
        
    public virtual SportsEvent SportsEvent { get; set; }
}