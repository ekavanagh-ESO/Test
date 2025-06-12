using Microsoft.EntityFrameworkCore;
using MythTechTest.Data;
using MythTechTest.Models.Entities;
using Newtonsoft.Json;

namespace MythTechTest.Services
{
    public class SportsEventIngestionService
    {
        private readonly HttpClient _httpClient;
        private readonly SportsEventContext _context;

        public SportsEventIngestionService(HttpClient httpClient, SportsEventContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task IngestDataFromEndpoint(string endpointUrl)
        {
            try
            {
                var response = await _httpClient.GetStringAsync(endpointUrl);
                var jsonEvents = JsonConvert.DeserializeObject<List<dynamic>>(response);

                foreach (var jsonEvent in jsonEvents)
                {
                    await ProcessSportsEvent(jsonEvent);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ingesting data: {ex.Message}");
                throw;
            }
        }

        private async Task ProcessSportsEvent(dynamic jsonEvent)
        {
            string eventId = jsonEvent.id;
            
            var existingEvent = await _context.SportsEvents
                .Include(e => e.States)
                .Include(e => e.SportsOrganizations)
                .Include(e => e.ParentEvents)
                .Include(e => e.RelatedEvents)
                .Include(e => e.Meta)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (existingEvent != null)
            {
                UpdateSportsEvent(existingEvent, jsonEvent);
            }
            else
            {
                CreateSportsEvent(jsonEvent);
            }
        }

        private void CreateSportsEvent(dynamic jsonEvent)
        {
            var sportsEvent = new SportsEvent
            {
                Id = jsonEvent.id,
                Description = jsonEvent.description,
                Type = jsonEvent.type,
                StartDateLocal = DateTime.Parse(jsonEvent.start_date_local.ToString()),
                ScheduledStartTimeUtc = DateTime.Parse(jsonEvent.scheduled_start_time_utc.ToString()),
                EndTime = DateTime.Parse(jsonEvent.end_time.ToString()),
                Status = jsonEvent.status,
                Attendance = (bool)jsonEvent.attendanceSpecified == true ? (int?)jsonEvent.attendance : null,
                SportId = jsonEvent.sport_id,
                VenueId = jsonEvent.venue_id,
                DirectParentSportsEventId = jsonEvent.direct_parent_sports_event_id,
                HomeParticipantId = jsonEvent.home_participant_id,
                AwayParticipantId = jsonEvent.away_participant_id,
                ParticipantType = jsonEvent.participant_type,
                States = new List<EventState>(),
                SportsOrganizations = new List<SportsOrganization>(),
                ParentEvents = new List<ParentEvent>(),
                RelatedEvents = new List<RelatedEvent>()
            };
            
            if (jsonEvent.state != null)
            {
                foreach (var state in jsonEvent.state)
                {
                    sportsEvent.States.Add(new EventState
                    {
                        Key = state.key,
                        Value = state.value
                    });
                }
            }
            
            if (jsonEvent.sports_organization_ids != null)
            {
                foreach (string orgId in jsonEvent.sports_organization_ids)
                {
                    sportsEvent.SportsOrganizations.Add(new SportsOrganization
                    {
                        OrganizationId = orgId
                    });
                }
            }
            
            if (jsonEvent.parent_sports_event_ids != null)
            {
                foreach (string parentId in jsonEvent.parent_sports_event_ids)
                {
                    sportsEvent.ParentEvents.Add(new ParentEvent
                    {
                        ParentEventId = parentId
                    });
                }
            }
            
            if (jsonEvent.related_sports_events != null)
            {
                foreach (var related in jsonEvent.related_sports_events)
                {
                    var relatedEvent = new RelatedEvent
                    {
                        RelatedEventId = related.id,
                        Type = related.type,
                        TypeDetail = related.type_detail,
                        Depth = related.depth
                    };

                    
                    if (related.navigation_info != null)
                    {
                        relatedEvent.NavigationInfo = new NavigationInfo
                        {
                            HasStandings = (bool?)related.navigation_info.has_standings,
                            IsKnockout = (bool?)related.navigation_info.is_knockout
                        };
                    }
                    else
                    {
                        
                        relatedEvent.NavigationInfo = new NavigationInfo
                        {
                            HasStandings = null,
                            IsKnockout = null
                        };
                    }

                    sportsEvent.RelatedEvents.Add(relatedEvent);
                }
            }

           
            if (jsonEvent.meta != null)
            {
                sportsEvent.Meta = new EventMeta
                {
                    SportsEventId = jsonEvent.id,
                    UpdateId = jsonEvent.meta.update_id,
                    UpdateAction = jsonEvent.meta.update_action,
                    UpdateDate = DateTime.Parse(jsonEvent.meta.update_date.ToString()),
                    Language = jsonEvent.meta.language
                };
            }

            _context.SportsEvents.Add(sportsEvent);
        }

        private void UpdateSportsEvent(SportsEvent existingEvent, dynamic jsonEvent)
        {
            
            existingEvent.Description = jsonEvent.description;
            existingEvent.Type = jsonEvent.type;
            existingEvent.Status = jsonEvent.status;
            existingEvent.Attendance = jsonEvent.attendance; 
            existingEvent.States.Clear();
            if (jsonEvent.state != null)
            {
                foreach (var state in jsonEvent.state)
                {
                    existingEvent.States.Add(new EventState
                    {
                        Key = state.key,
                        Value = state.value
                    });
                }
            }

            if (existingEvent.Meta != null && jsonEvent.meta != null)
            {
                existingEvent.Meta.UpdateId = jsonEvent.meta.update_id;
                existingEvent.Meta.UpdateAction = jsonEvent.meta.update_action;
                existingEvent.Meta.UpdateDate = DateTime.Parse(jsonEvent.meta.update_date.ToString());
            }
        }
    }
}