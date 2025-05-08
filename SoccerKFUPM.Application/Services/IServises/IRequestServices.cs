using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Enums;

namespace SoccerKFUPM.Application.Services.IServises;

public interface IRequestServices
{
    public Task<Result<bool>> CreateJoinTeamRequestAsync(JoinTeamForFirstTimeRequest joinTeamForFirstTimeRequest);
    public Task<Result<bool>> ProcessRequestJoinTeamForFirstTimeAsync(int requestId, int processorUserId, RequestStatus requestStatus, PlayerStatus playerStatus);

}