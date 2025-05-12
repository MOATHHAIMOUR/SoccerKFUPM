using AutoMapper;
using SoccerKFUPM.Application.DTOs.MatchDTOs;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Common.Mapping
{
    public class MatchMappingProfile : Profile
    {
        public MatchMappingProfile()
        {
            CreateMap<Match, UpcomingMatchDTO>()
                .ForMember(dest => dest.MatchScheduleId, opt => opt.MapFrom(src => src.MatchScheduleId))
                .ForMember(dest => dest.TeamAName, opt => opt.MapFrom(src => src.TeamAName))
                .ForMember(dest => dest.TeamBName, opt => opt.MapFrom(src => src.TeamBName))
                .ForMember(dest => dest.MatchDate, opt => opt.MapFrom(src => src.MatchDate))
                .ForMember(dest => dest.TournamentName, opt => opt.MapFrom(src => src.TournamentName));
        }
    }
}
