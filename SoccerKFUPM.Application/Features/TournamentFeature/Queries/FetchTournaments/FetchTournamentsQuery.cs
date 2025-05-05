using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TournamentDTOs;

namespace SoccerKFUPM.Application.Features.TournamentFeature.Queries.FetchTournaments;

public class FetchTournamentsQuery : IRequest<ApiResponse<List<TournamentDTO>>>
{
    public string? TournamentNumber { get; set; }
    public string? TournamentName { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public FetchTournamentsQuery(
        string? tournamentNumber = null,
        string? tournamentName = null,
        string? startDate = null,
        string? endDate = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        TournamentNumber = tournamentNumber;
        TournamentName = tournamentName;
        StartDate = startDate;
        EndDate = endDate;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
