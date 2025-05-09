using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.AuthDTOs;

namespace SoccerKFUPM.Application.Features.AuthFeature.Commands.ResetPasswordCommand
{
    public class ResetPasswordCommand : IRequest<ApiResponse<bool>>
    {
        public ResetPasswordCommand(ResetPasswordDTO resetPasswordDTO)
        {
            ResetPasswordDTO = resetPasswordDTO;
        }

        public ResetPasswordDTO ResetPasswordDTO { get; set; }
    }
}
