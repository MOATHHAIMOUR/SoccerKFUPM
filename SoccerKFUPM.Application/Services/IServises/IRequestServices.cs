using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Domain.Enums;

namespace SoccerKFUPM.Application.Services.IServises;

public interface IRequestServices
{
    Task<Result<bool>> CreateJoinTeamRequestAsync(int playerId, int teamId, PlayerPosition preferredPosition);
    Task<Result<bool>> IsPlayerEligibleForTeamJoinAsync(int playerId, int teamId);
}