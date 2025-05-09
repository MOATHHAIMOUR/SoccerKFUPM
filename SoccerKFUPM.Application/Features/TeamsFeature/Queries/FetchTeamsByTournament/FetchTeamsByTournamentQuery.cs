using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TeamDTOs;

namespace SoccerKFUPM.Application.Features.TeamsFeature.Queries.FetchTeamsByTournament;

public record FetchTeamsByTournamentQuery(
    int TournamentId,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<ApiResponse<List<TeamTournamentViewDTO>>>;
