using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.AuthDTOs;
using SoccerKFUPM.Application.Features.AuthFeature.Commands.Authentication;
using SoccerKFUPM.Application.Services.IServises;

public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, ApiResponse<AuthenticationResponseDTO>>
{

    private readonly IAuthenticationServices _authenticationServices;

    public AuthenticationCommandHandler(IAuthenticationServices authenticationServices)
    {
        _authenticationServices = authenticationServices;
    }

    public async Task<ApiResponse<AuthenticationResponseDTO>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
    {

        var result = await _authenticationServices.AuthenticateUser(request.AuthenticationRequest.Username, request.AuthenticationRequest.Password);

        if (!result.IsSuccess)
            return ApiResponseHandler.Unauthorized<AuthenticationResponseDTO>(result.Error.Message);

        return ApiResponseHandler.Success(result.Value!);

    }
}