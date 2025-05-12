using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;

namespace SoccerKFUPM.Application.Features.RefereeFeature.Commands.AssignRefereeToTournament;

public record AssignRefereeToTournamentCommand(int RefereeId, int TournamentId) : IRequest<ApiResponse<bool>>;
