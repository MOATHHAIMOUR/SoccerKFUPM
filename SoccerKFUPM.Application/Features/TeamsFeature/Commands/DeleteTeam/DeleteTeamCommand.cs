using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;

namespace SoccerKFUPM.Application.Features.TeamsFeature.Commands.DeleteTeam;

public record DeleteTeamCommand(int TeamId) : IRequest<ApiResponse<bool>>;