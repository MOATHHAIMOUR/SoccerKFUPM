using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.AuthDTOs;

namespace SoccerKFUPM.Application.Features.AuthFeature.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<ApiResponse<AuthenticationResponseDTO>>
    {
        public RefreshTokenCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public string RefreshToken { get; set; }
    }
}
