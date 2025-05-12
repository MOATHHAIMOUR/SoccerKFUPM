using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.RefereeDTOs;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Services.IServises;

public interface IRefereeServices
{
    public Task<Result<bool>> AddRefereeAsync(Referee referee, string usename, string IntialPassword);
    public Task<Result<List<RefereeViewDTO>>> GetAllRefereesAsync();
    public Task<Result<bool>> AssignRefereeToTournamentAsync(int refereeId, int tournamentId);
    public Task<Result<bool>> AssignRefereeToMatchAsync(int matchScheduleId, int tournamentRefereeId);

    public Task<Result<List<TournamentRefereeViewDTO>>> GetRefereesInTournamentAsync(int tournamentId);

}
