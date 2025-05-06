using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TeamDTOs;

namespace SoccerKFUPM.Application.Features.TeamsFeature.Queries.FetchTeams;

public record FetchTeamsQuery(
    string? Name = null,
    string? Address = null,
    string? Website = null,
    int? NumberOfPlayers = null,
    int? ManagerId = null,
    string? ManagerFirstName = null,
    string? ManagerLastName = null,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<ApiResponse<(List<TeamDTO> Teams, int TotalCount)>>;
