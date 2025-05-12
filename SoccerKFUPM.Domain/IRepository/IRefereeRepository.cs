using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;

namespace SoccerKFUPM.Domain.IRepository;

public interface IRefereeRepository
{
    public Task<bool> IsRefereeInSameMatchAsync(int matchScheduleId, int tournamentRefereeId);

    public Task<List<TournamentRefereeView>> GetRefereesInTournamentAsync(int tournamentId);
    public Task<int?> AddRefereeAsync(
       Referee Referee);
    public Task<bool> AssignTournamentRefereeToMatchAsync(int matchScheduleId, int tournamentRefereeId);
    public Task<List<RefereeView>> GetAllRefereesAsync();
    public Task<bool> AssignRefereeToTournamentAsync(int tournamentId, int refereeId);
    public Task<bool> IsRefereeExistsAsync(int refereeId);
    public Task<bool> IsRefereeInTournamentAsync(int refereeId, int tournamentId);
}
