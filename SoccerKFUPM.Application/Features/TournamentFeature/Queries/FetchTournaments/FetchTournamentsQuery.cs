using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TournamentDTOs;

namespace SoccerKFUPM.Application.Features.TournamentFeature.Queries.FetchTournaments;

public class FetchTournamentsQuery : IRequest<ApiResponse<List<TournamentDTO>>>
{
    public string? TournamentNumber { get; set; }
    public string? TournamentName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public FetchTournamentsQuery(
        string? tournamentNumber = null,
        string? tournamentName = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
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
