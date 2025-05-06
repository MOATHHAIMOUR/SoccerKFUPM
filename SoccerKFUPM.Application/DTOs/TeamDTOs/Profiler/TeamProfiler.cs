using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;

namespace SoccerKFUPM.Application.DTOs.TeamDTOs.Profiler
{
    public class TeamProfiler : AutoMapper.Profile
    {
        public TeamProfiler()
        {

            CreateMap<UpdateTeamDTO, Team>();
            CreateMap<Team, TeamDTO>();
            CreateMap<AddTeamDTO, Team>();

            CreateMap<TeamView, TeamDTO>();


        }
    }
}
