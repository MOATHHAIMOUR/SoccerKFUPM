using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.AuthDTOs;
namespace SoccerKFUPM.Application.Features.AuthFeature.Commands.Authentication;
public class AuthenticationCommand : IRequest<ApiResponse<AuthenticationResponseDTO>>
{
    public AuthenticationRequestDTO AuthenticationRequest { set; get; }
    public AuthenticationCommand(AuthenticationRequestDTO authenticationRequest)
    {
        AuthenticationRequest = authenticationRequest;
    }

}