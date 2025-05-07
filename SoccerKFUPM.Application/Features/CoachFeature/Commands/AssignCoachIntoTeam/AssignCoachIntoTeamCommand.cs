using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TeamDTOs;

namespace SoccerKFUPM.Application.Features.CoachFeature.Commands.AssignCoachIntoTeam;

public record AssignCoachIntoTeamCommand(AssignCoachIntoTeamDTO Dto) : IRequest<ApiResponse<bool>>
{
    public AssignCoachIntoTeamDTO AssignCoachIntoTeamDTO { get; set; } = Dto;
}