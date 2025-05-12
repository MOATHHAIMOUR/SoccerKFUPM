using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;

namespace SoccerKFUPM.Application.Features.RefereeFeature.Commands.AssignRefereeToMatchInTournament;

public record AssignRefereeToMatchInTournamentCommand(int tournamentRefereeId, int MatchScheduleId) : IRequest<ApiResponse<bool>>;
