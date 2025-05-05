using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;

namespace SoccerKFUPM.Application.Features.TournamentFeature.Commands.DeleteTournament;

public record DeleteTournamentCommand(int TournamentId) : IRequest<ApiResponse<bool>>;