using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;

namespace Namespace.SoccerKFUPM.Domain.IRepository;

public interface IPlayerRepository
{
    public Task<bool> IsUserAlreadyPlayerAsync(int userId);
    Task<bool> AddPlayerAsync(Player player);
    Task<(List<PlayerView> Players, int TotalCount)> GetAllPlayersAsync(int? playerId,
    string? kfupmId,
    int pageNumber,
    int pageSize);
    Task<Player?> GetPlayerByIdAsync(int playerId);
    Task<bool> AssignPlayerToTeamAsync(PlayerTeam playerTeams);

    public Task<bool> IsPlayerAlreadyAssignedAsync(int playerId, int tournamentId);


}