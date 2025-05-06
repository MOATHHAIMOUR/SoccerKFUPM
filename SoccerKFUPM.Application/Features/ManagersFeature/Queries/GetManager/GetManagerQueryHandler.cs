using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.ManagerDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.ManagersFeature.Queries.GetManager;

public class GetManagerQueryHandler : IRequestHandler<GetManagerQuery, ApiResponse<ManagerDTO>>
{
    private readonly IManagerServices _managerServices;

    public GetManagerQueryHandler(IManagerServices managerServices)
    {
        _managerServices = managerServices;
    }

    public async Task<ApiResponse<ManagerDTO>> Handle(GetManagerQuery request, CancellationToken cancellationToken)
    {
        var result = await _managerServices.GetManagerByIdAsync(request.ManagerId);
        return ApiResponseHandler.Build(result.Value, result.StatusCode, result.IsSuccess, null, [result.Error.Message]);
    }
}