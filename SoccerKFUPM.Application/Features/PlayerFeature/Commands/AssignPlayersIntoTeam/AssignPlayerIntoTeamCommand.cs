using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.PlayerDTOs;

namespace SoccerKFUPM.Application.Features.PlayerFeature.Commands.AssignPlayersIntoTeam;

public record AssignPlayerIntoTeamCommand(AssignPlayerIntoTeamDTO Dto) : IRequest<ApiResponse<bool>>
{
    public AssignPlayerIntoTeamDTO AssignPlayerIntoTeamDTO { get; set; } = Dto;
}