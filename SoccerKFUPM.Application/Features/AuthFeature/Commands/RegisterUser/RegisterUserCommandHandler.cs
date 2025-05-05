using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.AuthDTOs;
using SoccerKFUPM.Application.Services.IServises;
namespace SoccerKFUPM.Application.Features.AuthFeature.Commands.RegisterUser;


public class RegisterUserCommandHandler(IAuthenticationServices authenticationServices) : IRequestHandler<RegisterUserCommand, ApiResponse<AuthenticationResponseDTO>>
{
    private readonly IAuthenticationServices _authenticationServices = authenticationServices;

    async Task<ApiResponse<AuthenticationResponseDTO>> IRequestHandler<RegisterUserCommand, ApiResponse<AuthenticationResponseDTO>>.Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _authenticationServices.RegisterUserAsync(request.RegisterUserDTO);

        return ApiResponseHandler.Build(result.Value, result.StatusCode, result.IsSuccess, null, [result.Error.Message]);

    }
}