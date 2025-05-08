using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities.Enums;

namespace SoccerKFUPM.Application.Features.RequestsFeature.Commands.ProcessJoinTeamRequest
{
    public class ProcessJoinTeamRequestCommandHandler : IRequestHandler<ProcessJoinTeamRequestCommand, ApiResponse<bool>>
    {
        private readonly IRequestServices _requestServices;

        public ProcessJoinTeamRequestCommandHandler(IRequestServices requestServices)
        {
            _requestServices = requestServices;
        }

        public async Task<ApiResponse<bool>> Handle(ProcessJoinTeamRequestCommand request, CancellationToken cancellationToken)
        {

            var result = await _requestServices.ProcessRequestJoinTeamForFirstTimeAsync(
                requestId: request.ProcessJoinTeamRequestDTO.RequestId,
                processorUserId: request.ProcessJoinTeamRequestDTO.ProcessorUserId,
                requestStatus: (RequestStatus)request.ProcessJoinTeamRequestDTO.RequestStatus,
                playerStatus: (PlayerStatus)request.ProcessJoinTeamRequestDTO.PlayerStatus
            );

            return ApiResponseHandler.Build(
                result.Value,
                result.StatusCode,
                result.IsSuccess,
                null,
                [result.Error.Message]
            );
        }
    }
}
