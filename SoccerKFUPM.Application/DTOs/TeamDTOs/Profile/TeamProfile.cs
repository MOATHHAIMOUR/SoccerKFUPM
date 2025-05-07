using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.DTOs.TeamDTOs.Profile;

public class TeamProfile : AutoMapper.Profile
{
    public TeamProfile()
    {

        CreateMap<AssignCoachIntoTeamDTO, CoachTeam>()
            .ForMember(dest => dest.JoinedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}