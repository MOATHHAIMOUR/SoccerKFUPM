using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TeamDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.TeamsFeature.Queries.FetchTeams;

public class FetchTeamsQueryHandler : IRequestHandler<FetchTeamsQuery, ApiResponse<(List<TeamDTO> Teams, int TotalCount)>>
{
    private readonly ITeamServices _teamServices;

    public FetchTeamsQueryHandler(ITeamServices teamServices)
    {
        _teamServices = teamServices;
    }

    public async Task<ApiResponse<(List<TeamDTO> Teams, int TotalCount)>> Handle(FetchTeamsQuery request, CancellationToken cancellationToken)
    {
        var result = await _teamServices.SearchTeamsAsync(
            name: request.Name,
            address: request.Address,
            website: request.Website,
            numberOfPlayers: request.NumberOfPlayers,
            managerId: request.ManagerId,
            managerFirstName: request.ManagerFirstName,
            managerLastName: request.ManagerLastName,
            pageNumber: request.PageNumber,
            pageSize: request.PageSize
        );

        return ApiResponseHandler.Build(
            data: result.Value,
            statusCode: result.StatusCode,
            succeeded: result.IsSuccess,
            message: result.IsSuccess ? "Teams fetched successfully" : result.Error?.Message,
            errors: result.IsSuccess ? null : [result.Error?.Message ?? "Unknown error"]
        );
    }

}