using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TournamentDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.TournamentFeature.Queries.FetchTournaments;

public class FetchTournamentsQueryHandler : IRequestHandler<FetchTournamentsQuery, ApiResponse<List<TournamentDTO>>>
{
    private readonly ITournamentServices _tournamentServices;

    public FetchTournamentsQueryHandler(ITournamentServices tournamentServices)
    {
        _tournamentServices = tournamentServices;
    }

    public async Task<ApiResponse<List<TournamentDTO>>> Handle(FetchTournamentsQuery request, CancellationToken cancellationToken)
    {
        var result = await _tournamentServices.GetAllTournamentsAsync(request.TournamentNumber, request.TournamentName, request.StartDate, request.EndDate, request.PageNumber, request.PageSize);

        return ApiResponseHandler.Build(
            data: result.Value.tournaments ,
            statusCode: result.StatusCode,
            succeeded: result.IsSuccess,
            errors: result.IsSuccess ? null : [result.Error.Message],
            meta: new { TotalCount = result.Value.totalCount }
        );
    }
}