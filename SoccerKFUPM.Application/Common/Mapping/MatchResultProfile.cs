using AutoMapper;
using SoccerKFUPM.Application.DTOs.MatchDTOs;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Common.Mapping;

public class MatchResultProfile : Profile
{
    public MatchResultProfile()
    {
        CreateMap<AddMatchRecordDTO, MatchRecord>();
        CreateMap<TeamMatchRecordDTO, MatchRecord>();
        CreateMap<ShotOnGoalDTO, ShotOnGoal>();
    }
}