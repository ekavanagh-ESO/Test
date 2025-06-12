using AutoMapper;
using MythTechTest.Models.DTOs;
using MythTechTest.Models.Entities;

namespace MythTechTest.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SportsEvent, SportsEventDto>()
            .ForMember(dest => dest.State, opt => opt.MapFrom(src =>
                src.States.ToDictionary(s => s.Key, s => s.Value)))
            .ForMember(dest => dest.SportsOrganizationIds, opt => opt.MapFrom(src =>
                src.SportsOrganizations.Select(so => so.OrganizationId).ToList()))
            .ForMember(dest => dest.ParentSportsEventIds, opt => opt.MapFrom(src =>
                src.ParentEvents.Select(pe => pe.ParentEventId).ToList()))
            .ForMember(dest => dest.RelatedSportsEvents, opt => opt.MapFrom(src =>
                src.RelatedEvents));

        CreateMap<RelatedEvent, RelatedEventDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RelatedEventId))
            .ForMember(dest => dest.NavigationInfo, opt => opt.MapFrom(src => new NavigationInfoDto
            {
                HasStandings = src.NavigationInfo.HasStandings,
                IsKnockout = src.NavigationInfo.IsKnockout
            }));

        CreateMap<EventMeta, EventMetaDto>();
    }
}