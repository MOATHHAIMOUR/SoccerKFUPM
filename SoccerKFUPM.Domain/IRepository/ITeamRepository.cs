using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;

namespace SoccerKFUPM.Domain.IRepository;

public interface ITeamRepository
{
    public Task<bool> TeamExistsAsync(int teamId);
    Task<bool> AddTeamAsync(Team team);
    Task<bool> DeleteTeamAsync(int teamId);
    public Task<(List<TeamView> teams, int totalCount)> SearchTeamsAsync(
        string? name = null,
        string? address = null,
        string? website = null,
        int? numberOfPlayers = null,
        int? managerId = null,
        string? managerFirstName = null,
        string? managerLastName = null,
        int pageNumber = 1,
        int pageSize = 10);
    Task<TeamView?> GetTeamByIdAsync(int teamId);
    Task<bool> UpdateTeamAsync(Team team);
    Task<bool> AssignCoachToTeamAsync(CoachTeam coachTeam);

    public Task<bool> IsTeamInTournamentAsync(int teamId, int tournamentId);

    public Task<bool> IsCoachAlreadyAssignedAsync(int coachId, int tournamentId);
}