using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.TeamsFeature.Commands.DeleteTeam;

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, ApiResponse<bool>>
{
    private readonly ITeamServices _teamServices;

    public DeleteTeamCommandHandler(ITeamServices teamServices)
    {
        _teamServices = teamServices;
    }

    public async Task<ApiResponse<bool>> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var result = await _teamServices.DeleteTeamAsync(request.TeamId);
        return ApiResponseHandler.Build(result.Value, result.StatusCode, result.IsSuccess, null, [result.Error.Message]);
    }
}