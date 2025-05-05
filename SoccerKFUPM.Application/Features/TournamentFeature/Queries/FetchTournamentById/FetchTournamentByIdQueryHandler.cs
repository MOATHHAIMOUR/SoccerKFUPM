using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TournamentDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.TournamentFeature.Queries.FetchTournamentById
{
    public class FetchTournamentByIdQueryHandler : IRequestHandler<FetchTournamentByIdQuery, ApiResponse<TournamentDTO>>
    {
        private readonly ITournamentServices _tournamentServices;

        public FetchTournamentByIdQueryHandler(ITournamentServices tournamentServices)
        {
            _tournamentServices = tournamentServices;
        }

        public async Task<ApiResponse<TournamentDTO>> Handle(FetchTournamentByIdQuery request, CancellationToken cancellationToken)
        {
            var tournament = await _tournamentServices.GetTournamentByIdAsync(request.TournamentId);

            return ApiResponseHandler.Build(
                data: tournament.Value,
                statusCode: tournament.StatusCode,
                succeeded: tournament.IsSuccess,
                errors: [tournament.Error.Message]
            );


        }
    }
}
