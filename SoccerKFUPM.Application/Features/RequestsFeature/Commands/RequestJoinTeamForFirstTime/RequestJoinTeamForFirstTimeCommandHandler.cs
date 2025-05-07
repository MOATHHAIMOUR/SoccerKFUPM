using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;
using System.Net;

namespace SoccerKFUPM.Application.Features.RequestsFeature.Commands.RequestJoinTeamForFirstTime;

public class RequestJoinTeamForFirstTimeCommandHandler : IRequestHandler<RequestJoinTeamForFirstTimeCommand, ApiResponse<bool>>
{
    private readonly IRequestServices _requestServices;
    private readonly ITeamServices _teamServices;
    private readonly IPlayerServices _playerServices;
    private readonly IMapper _mapper;

    public RequestJoinTeamForFirstTimeCommandHandler(
        IRequestServices requestServices,
        ITeamServices teamServices,
        IPlayerServices playerServices,
        IMapper mapper)
    {
        _requestServices = requestServices;
        _teamServices = teamServices;
        _playerServices = playerServices;
        _mapper = mapper;
    }

    public async Task<ApiResponse<bool>> Handle(RequestJoinTeamForFirstTimeCommand request, CancellationToken cancellationToken)
    {
        // Validate that player exists
        var playerResult = await _playerServices.GetPlayerByIdAsync(request.RequestJoinTeamDTO.PlayerId);
        if (!playerResult.IsSuccess)
        {
            return ApiResponseHandler.Build<bool>(
                false,
                HttpStatusCode.NotFound,
                false,
                null,
                ["Player not found"]
            );
        }

        // Validate that team exists
        var teamResult = await _teamServices.GetTeamByIdAsync(request.RequestJoinTeamDTO.TeamId);
        if (!teamResult.IsSuccess)
        {
            return ApiResponseHandler.Build<bool>(
                false,
                HttpStatusCode.NotFound,
                false,
                null,
                ["Team not found"]
            );
        }

        // Create and submit the join request
        var result = await _requestServices.CreateJoinTeamRequestAsync(
            request.RequestJoinTeamDTO.PlayerId,
            request.RequestJoinTeamDTO.TeamId,
            request.RequestJoinTeamDTO.PreferredPosition);

        return ApiResponseHandler.Build(
            result.Value,
            result.StatusCode,
            result.IsSuccess,
            result.IsSuccess ? "Team join request submitted successfully" : null,
            result.IsSuccess ? null : [result.Error.Message]
        );
    }
}