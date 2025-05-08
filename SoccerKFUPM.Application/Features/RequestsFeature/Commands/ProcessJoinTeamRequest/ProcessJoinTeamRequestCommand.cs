using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.RequestDTOs;

namespace SoccerKFUPM.Application.Features.RequestsFeature.Commands.ProcessJoinTeamRequest
{
    public class ProcessJoinTeamRequestCommand : IRequest<ApiResponse<bool>>
    {
        public ProcessJoinTeamRequestDTO ProcessJoinTeamRequestDTO { get; set; }

        public ProcessJoinTeamRequestCommand(ProcessJoinTeamRequestDTO processJoinTeamRequestDTO)
        {
            ProcessJoinTeamRequestDTO = processJoinTeamRequestDTO;
        }
    }
}
