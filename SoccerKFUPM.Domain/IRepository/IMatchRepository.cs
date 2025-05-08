using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;

namespace SoccerKFUPM.Domain.IRepository;

public interface IMatchRepository
{
    Task<bool> ScheduleMatchAsync(MatchSchedule match);
    public Task<(List<MatchView> Matches, int TotalCount)> SearchMatchesAsync(
int? tournamentId = null,
int? tournamentPhase = null,
string? teamAName = null,
string? teamBName = null,
string? fieldName = null,
DateTime? matchDate = null,
int pageNumber = 1,
int pageSize = 10);
}