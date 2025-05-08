using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Domain.Entities.Views;

namespace SoccerKFUPM.Application.Features.MatchFeature.Queries.GetAllScheduledMatches;

public record GetAllScheduledMatchesQuery(
    int? TournamentId = null,
    int? TournamentPhase = null,
    string? TeamAName = null,
    string? TeamBName = null,
    string? FieldName = null,
    DateTime? MatchDate = null,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<ApiResponse<List<MatchView>>>;
