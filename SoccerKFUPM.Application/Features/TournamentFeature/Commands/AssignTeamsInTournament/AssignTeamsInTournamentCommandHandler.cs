using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.TournamentFeature.Commands.AssignTeamsInTournament;

public class AssignTeamsInTournamentCommandHandler : IRequestHandler<AssignTeamsInTournamentCommand, ApiResponse<bool>>
{
    private readonly ITournamentServices _tournamentServices;

    public AssignTeamsInTournamentCommandHandler(ITournamentServices tournamentServices)
    {
        _tournamentServices = tournamentServices;
    }

    public async Task<ApiResponse<bool>> Handle(AssignTeamsInTournamentCommand request, CancellationToken cancellationToken)
    {
        var result = await _tournamentServices.AssignTeamsToTournamentAsync(
            request.AssignTeamsInTournamentDTO.TournamentId,
            request.AssignTeamsInTournamentDTO.TeamIds);

        return ApiResponseHandler.Build(
            data: result.Value,
            statusCode: result.StatusCode,
            succeeded: result.IsSuccess,
            message: result.IsSuccess ? "Teams assigned to tournament successfully" : result.Error.Message,
            errors: result.IsSuccess ? null : [result.Error.Message]
        );
    }
}