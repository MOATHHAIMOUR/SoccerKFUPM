using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Domain.IRepository;

public interface ITournamentRepository
{
    public  Task<int> AddTournamentAsync(Tournament tournament);
    public Task<Tournament?> GetTournamentByIdAsync(int tournamentId);
    public Task<bool> UpdateTournamentAsync(Tournament tournament);
    public Task<bool> DeleteTournamentAsync(int tournamentId);
    public Task<(List<Tournament> Tournaments, int TotalCount)> SearchTournamentsAsync(
            string? number, string? name, string? startDate, string? endDate, int pageNumber, int pageSize);
}