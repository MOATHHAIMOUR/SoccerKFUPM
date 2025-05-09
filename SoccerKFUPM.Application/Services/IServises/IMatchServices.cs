using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;

namespace SoccerKFUPM.Application.Services.IServises;

public interface IMatchServices
{

    public Task<Result<(List<MatchView> matches, int totalCount)>> GetAllScheduledMatchesAsync(
    int? tournamentId = null,
    int? tournamentPhase = null,
    string? teamAName = null,
    string? teamBName = null,
    string? fieldName = null,
    DateTime? matchDate = null,
    int pageNumber = 1,
    int pageSize = 10);

    Task<Result<bool>> ScheduleMatchAsync(MatchSchedule match);

    Task<Result<bool>> AddMatchResultAsync(MatchRecord matchResult);

}