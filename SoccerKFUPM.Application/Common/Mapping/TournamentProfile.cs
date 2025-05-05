using AutoMapper;
using SoccerKFUPM.Application.DTOs.TournamentDTOs;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Common.Mapping;

public class TournamentProfile : Profile
{
    public TournamentProfile()
    {
        CreateMap<Tournament, TournamentDTO>();
        CreateMap<AddTournamentDTO, Tournament>();
    }
}