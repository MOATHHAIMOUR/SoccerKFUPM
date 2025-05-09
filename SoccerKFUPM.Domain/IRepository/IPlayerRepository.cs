using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;

namespace Namespace.SoccerKFUPM.Domain.IRepository;

public interface IPlayerRepository
{
    public Task<bool> IsUserAlreadyPlayerAsync(int userId);
    public Task<int?> AddPlayerAsync(Player player);
    public Task<(List<PlayerView> Players, int TotalCount)> GetAllPlayersAsync(int? playerId,
    string? kfupmId,
    int pageNumber,
    int pageSize);
    public Task<Player?> GetPlayerByIdAsync(int playerId);
    public Task<bool> AssignPlayerToTeamAsync(PlayerTeam playerTeams);

    public Task<bool> IsPlayerAlreadyAssignedAsync(int playerId, int tournamentId);


}