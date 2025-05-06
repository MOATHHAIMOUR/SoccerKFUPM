using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TeamDTOs;

namespace SoccerKFUPM.Application.Features.TeamsFeature.Commands.UpdateTeam;

public class UpdateTeamCommand : IRequest<ApiResponse<bool>>
{
    public UpdateTeamCommand(UpdateTeamDTO updateTeamDTO)
    {
        UpdateTeamDTO = updateTeamDTO;
    }

    public UpdateTeamDTO UpdateTeamDTO { get; set; }
}