namespace SoccerKFUPM.Application.Services.IServises;

using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.PlayerDTOs;
using SoccerKFUPM.Domain.Entities;

public interface IPlayerServices
{
    public Task<Result<bool>> AddPlayerAsync(Player player);


    public Task<Result<(List<PlayerDTO> playerDTOs, int totalCount)>> GetAllPlayersAsync(int? playerId,
    string? kfupmId,
    int pageNumber = 1,
    int pageSize = 10);


    public Task<Result<PlayerDTO>> GetPlayerByIdAsync(int playerId);

    public Task<Result<bool>> AssignPlayerToTeamAsync(PlayerTeam playerTeam, int tournamentId);
    //Task<Result<bool>> AssignCoachToTeamAsync(CoachTeam coachTeam);

}