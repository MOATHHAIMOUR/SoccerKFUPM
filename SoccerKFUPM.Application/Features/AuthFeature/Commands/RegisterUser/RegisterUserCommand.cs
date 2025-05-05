using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.AuthDTOs;

namespace SoccerKFUPM.Application.Features.AuthFeature.Commands.RegisterUser;   
public class RegisterUserCommand(RegsterAccountRequestDTO registerUserDTO) : IRequest<ApiResponse<AuthenticationResponseDTO>>
{
    public RegsterAccountRequestDTO RegisterUserDTO { get; set; } = registerUserDTO;
}
