using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Enums;

namespace SoccerKFUPM.Domain.IRepository;

public interface IRequestRepository
{
    Task<bool> CreateRequestAsync(Request request);
    Task<Request?> GetRequestByIdAsync(int requestId);
    Task<bool> HasPendingTeamRequestAsync(int playerId, int teamId);
    Task<bool> IsPlayerInTeamAsync(int playerId, int teamId);
    Task<(List<Request> Requests, int TotalCount)> GetRequestsByPlayerAsync(
        int playerId,
        int pageNumber = 1,
        int pageSize = 10);
    Task<(List<Request> Requests, int TotalCount)> GetRequestsByTeamAsync(
        int teamId,
        int pageNumber = 1,
        int pageSize = 10);
}