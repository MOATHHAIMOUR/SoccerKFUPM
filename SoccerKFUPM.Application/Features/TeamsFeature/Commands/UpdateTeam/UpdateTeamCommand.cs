using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TeamDTOs;

namespace SoccerKFUPM.Application.Features.TeamsFeature.Commands.UpdateTeam;

public record UpdateTeamCommand(UpdateTeamDTO dto) : IRequest<ApiResponse<bool>>
{
    public UpdateTeamDTO UpdateTeamDTO { get; set; } = dto;
}