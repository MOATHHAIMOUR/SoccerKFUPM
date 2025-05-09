using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.CoachDTOs;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Services.IServises;

public interface ICoachServices
{

    public Task<Result<bool>> AssignCoachToTeamAsync(CoachTeam coachTeam, int tournamentId);

    public Task<Result<bool>> AddCoachAsync(Coache coach, string username, string initialPassword);
    public Task<Result<(List<CoachViewDTO> coaches, int totalCount)>> GetCoachesAsync(
    string? kfupmId = null,
    string? firstName = null,
    string? secondName = null,
    string? thirdName = null,
    string? lastName = null,
    DateTime? dateOfBirth = null,
    int? nationalityId = null,
    string? teamName = null,
    bool? isActive = null,
    int pageNumber = 1,
    int pageSize = 10);
}