using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.RefereeDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.RefereeFeature.Queries.GetAllReferees;

public class GetAllRefereesQueryHandler : IRequestHandler<GetAllRefereesQuery, ApiResponse<List<RefereeViewDTO>>>
{
    private readonly IRefereeServices _refereeServices;

    public GetAllRefereesQueryHandler(IRefereeServices refereeServices)
    {
        _refereeServices = refereeServices;
    }

    public async Task<ApiResponse<List<RefereeViewDTO>>> Handle(GetAllRefereesQuery request, CancellationToken cancellationToken)
    {
        var result = await _refereeServices.GetAllRefereesAsync();
        return ApiResponseHandler.Build(result.Value, result.StatusCode, result.IsSuccess);
    }
}
