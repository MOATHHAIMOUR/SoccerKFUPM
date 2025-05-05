using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.AuthDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.AuthFeature.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ApiResponse<AuthenticationResponseDTO>>
    {
        private readonly IAuthenticationServices _authenticationService;

        public RefreshTokenCommandHandler(IAuthenticationServices authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<ApiResponse<AuthenticationResponseDTO>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.RefreshTokenAsync(request.RefreshToken);

            return ApiResponseHandler.Build(result.Value, result.StatusCode, result.IsSuccess, null, [result.Error.Message]);
        }
    }
}
