using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TeamDTOs;

namespace SoccerKFUPM.Application.Features.TeamsFeature.Queries.GetTeamInfoById
{
    public class GetTeamInfoByIdQuery : IRequest<ApiResponse<TeamViewDTO>>
    {
        public GetTeamInfoByIdQuery(int teamId)
        {
            TeamId = teamId;
        }

        public int TeamId { set; get; }
    }
}
