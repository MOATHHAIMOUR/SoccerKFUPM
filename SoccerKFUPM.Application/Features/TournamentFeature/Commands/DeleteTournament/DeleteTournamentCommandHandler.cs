using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.TournamentFeature.Commands.DeleteTournament;

public class DeleteTournamentCommandHandler : IRequestHandler<DeleteTournamentCommand, ApiResponse<bool>>
{
    private readonly ITournamentServices _tournamentServices;

    public DeleteTournamentCommandHandler(ITournamentServices tournamentServices)
    {
        _tournamentServices = tournamentServices;
    }

    public async Task<ApiResponse<bool>> Handle(DeleteTournamentCommand request, CancellationToken cancellationToken)
    {
        var result = await _tournamentServices.DeleteTournamentAsync(request.TournamentId);

        return ApiResponseHandler.Build(
            data: result.Value,
            statusCode: result.StatusCode,
            succeeded: result.IsSuccess,
            message: result.IsSuccess ? "Tournament deleted successfully" : result.Error.Message,
            errors: result.IsSuccess ? null : [result.Error.Message]
        );
    }
}