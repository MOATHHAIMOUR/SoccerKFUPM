using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TeamDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.TeamsFeature.Queries.GetTeamInfoById
{
    public class GetTeamInfoByIdQueryHandler : IRequestHandler<GetTeamInfoByIdQuery, ApiResponse<TeamViewDTO>>
    {
        private readonly ITeamServices _teamServices;

        public GetTeamInfoByIdQueryHandler(ITeamServices teamServices)
        {
            _teamServices = teamServices;
        }


        async Task<ApiResponse<TeamViewDTO>> IRequestHandler<GetTeamInfoByIdQuery, ApiResponse<TeamViewDTO>>.Handle(GetTeamInfoByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _teamServices.GetTeamByIdAsync(request.TeamId);

            return ApiResponseHandler.Success(result.Value);
        }
    }
}
