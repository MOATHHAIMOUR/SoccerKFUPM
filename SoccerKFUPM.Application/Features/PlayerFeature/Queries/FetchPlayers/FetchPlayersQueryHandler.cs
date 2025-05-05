using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.PlayerDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.PlayerFeature.Queries.FetchPlayers;

public class FetchPlayersQueryHandler : IRequestHandler<FetchPlayersQuery, ApiResponse<List<PlayerDTO>>>
{
    private readonly IPlayerServices _playerServices;

    public FetchPlayersQueryHandler(IPlayerServices playerServices)
    {
        _playerServices = playerServices;
    }

    public async Task<ApiResponse<List<PlayerDTO>>> Handle(FetchPlayersQuery request, CancellationToken cancellationToken)
    {
        var result = await _playerServices.GetAllPlayersAsync(request.PlayerId, request.KfupmId, request.PageNumber, request.PageSize);

        var (playerDTOs, totalCount) = result.Value;


        return ApiResponseHandler.Build<List<PlayerDTO>>(playerDTOs, result.StatusCode, result.IsSuccess, null, null, new
        {
            TotalCount = totalCount,
        });
    }
}