using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.ManagerDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.ManagersFeature.Queries.SearchManagers
{
    public class SearchManagersQueryHandler : IRequestHandler<SearchManagersQuery, ApiResponse<List<ManagerSearchViewDTO>>>
    {
        private readonly IManagerServices _managerService;

        public SearchManagersQueryHandler(IManagerServices managerService)
        {
            _managerService = managerService;
        }

        public async Task<ApiResponse<List<ManagerSearchViewDTO>>> Handle(SearchManagersQuery request, CancellationToken cancellationToken)
        {
            var result = await _managerService.SearchManagersAsync(
                request.KFUPMId,
                request.FirstName,
                request.SecondName,
                request.ThirdName,
                request.LastName,
                request.DateOfBirth,
                request.NationalityId,
                request.TeamName,
                request.PageNumber,
                request.PageSize
            );

            return ApiResponseHandler.Build(
                data: result.Value.Managers,
                statusCode: result.StatusCode,
                succeeded: result.IsSuccess,
                message: result.IsSuccess ? "Managers retrieved successfully" : result.Error?.Message,
                errors: result.IsSuccess ? null : [result.Error?.Message],
                meta: new
                {
                    count = result.Value.TotalCount
                }

            );
        }
    }
}
