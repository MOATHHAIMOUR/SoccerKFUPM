using SoccerKFUPM.Application.Common.Errors;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Enums;
using SoccerKFUPM.Domain.Enums;
using SoccerKFUPM.Domain.IRepository;
using System.Net;

namespace SoccerKFUPM.Application.Services;

public class RequestServices : IRequestServices
{
    private readonly IRequestRepository _requestRepository;

    public RequestServices(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }

    public async Task<Result<bool>> CreateJoinTeamRequestAsync(int playerId, int teamId, PlayerPosition preferredPosition)
    {
        // Check if player is eligible to join
        var eligibilityResult = await IsPlayerEligibleForTeamJoinAsync(playerId, teamId);
        if (!eligibilityResult.IsSuccess)
        {
            return eligibilityResult;
        }

        // Create the request
        var request = new Request
        {
            PlayerId = playerId,
            TeamId = teamId,
            RequestType = RequestType.JoinTeam,
            Status = RequestStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            PreferredPosition = preferredPosition
        };

        bool result = await _requestRepository.CreateRequestAsync(request);
        return Result<bool>.Success(result);
    }

    public async Task<Result<bool>> IsPlayerEligibleForTeamJoinAsync(int playerId, int teamId)
    {
        // Check if player is already in the team
        bool isInTeam = await _requestRepository.IsPlayerInTeamAsync(playerId, teamId);
        if (isInTeam)
        {
            return Result<bool>.Failure(
                Error.ValidationError("Player is already a member of this team"),
                HttpStatusCode.BadRequest);
        }

        // Check for pending requests
        bool hasPendingRequest = await _requestRepository.HasPendingTeamRequestAsync(playerId, teamId);
        if (hasPendingRequest)
        {
            return Result<bool>.Failure(
                Error.ValidationError("Player already has a pending request for this team"),
                HttpStatusCode.BadRequest);
        }

        return Result<bool>.Success(true);
    }
}