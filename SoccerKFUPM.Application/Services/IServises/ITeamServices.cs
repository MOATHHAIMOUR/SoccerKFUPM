using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TeamDTOs;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Services.IServises;

public interface ITeamServices
{
    Task<Result<bool>> AddTeamAsync(Team team);
    Task<Result<TeamDTO>> GetTeamByIdAsync(int teamId);
    Task<Result<(List<TeamDTO> teams, int totalCount)>> SearchTeamsAsync(
     string? name = null,
     string? address = null,
     string? website = null,
     int? numberOfPlayers = null,
     int? managerId = null,
     string? managerFirstName = null,
     string? managerLastName = null,
     int pageNumber = 1,
     int pageSize = 10);

    Task<Result<bool>> UpdateTeamAsync(Team team);
    Task<Result<bool>> DeleteTeamAsync(int teamId);
    public Task<Result<bool>> AssignCoachToTeamAsync(CoachTeam coachTeam, int tournamentId);
}