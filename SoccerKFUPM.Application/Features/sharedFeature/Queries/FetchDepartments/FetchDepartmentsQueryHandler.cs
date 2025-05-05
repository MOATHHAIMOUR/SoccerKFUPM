using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Application.DTOs.SharedDTOs;

namespace SoccerKFUPM.Application.Features.sharedFeature.Queries.FetchDepartments;

public class FetchDepartmentsQueryHandler : IRequestHandler<FetchDepartmentsQuery, ApiResponse<List<DepartmentDTO>>>
{
    private readonly ISharedServices _sharedServices;

    public FetchDepartmentsQueryHandler(ISharedServices sharedServices)
    {
        _sharedServices = sharedServices;
    }

    public async Task<ApiResponse<List<DepartmentDTO>>> Handle(FetchDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var departments = await _sharedServices.GetAllDepartmentsAsync();
        return ApiResponseHandler.Success(departments.Value ?? []);
    }
}