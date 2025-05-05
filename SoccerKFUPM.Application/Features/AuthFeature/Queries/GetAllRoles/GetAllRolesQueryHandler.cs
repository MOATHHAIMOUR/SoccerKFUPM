using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.AuthDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.AuthFeature.Queries.GetAllRoles;

public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, ApiResponse<List<RoleDTO>>>
{
    private readonly IAuthenticationServices _authentication;

    public GetAllRolesQueryHandler(IAuthenticationServices authenticationServices)
    {
        _authentication = authenticationServices;
    }

    public async Task<ApiResponse<List<RoleDTO>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var result = await _authentication.GetAllRolesAsync();

        return ApiResponseHandler.Build(result.Value, result.StatusCode, result.IsSuccess, null, [result.Error.Message]);

    }
}