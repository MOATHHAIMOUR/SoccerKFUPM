using AutoMapper;
using SoccerKFUPM.Application.DTOs.MatchDTOs;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;

namespace SoccerKFUPM.Application.Common.Mapping;

public class MatchProfile : Profile
{
    public MatchProfile()
    {
        CreateMap<MatchSchedule, MatchScheduleDTO>();

        CreateMap<MatchView, MatchDTO>();

    }
}