using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TeamDTOs;

namespace SoccerKFUPM.Application.Features.TeamsFeature.Commands.AddTeam;

public record AddTeamCommand(AddTeamDTO dto) : IRequest<ApiResponse<bool>>
{
    public AddTeamDTO AddTeamDTO { get; set; } = dto;
}