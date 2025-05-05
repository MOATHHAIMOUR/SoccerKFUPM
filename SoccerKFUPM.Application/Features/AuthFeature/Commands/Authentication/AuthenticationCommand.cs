using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.AuthDTOs;
namespace SoccerKFUPM.Application.Features.AuthFeature.Commands.Authentication;
public class AuthenticationCommand : IRequest<ApiResponse<AuthenticationResponseDTO>>
{
    public AuthenticationRequest AuthenticationRequest { set; get; }
    public AuthenticationCommand(AuthenticationRequest authenticationRequest)
    {
        AuthenticationRequest = authenticationRequest;
    }

}