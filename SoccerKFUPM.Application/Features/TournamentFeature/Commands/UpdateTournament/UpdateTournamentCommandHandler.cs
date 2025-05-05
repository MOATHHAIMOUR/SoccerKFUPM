using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Features.TournamentFeature.Commands.UpdateTournament
{
    public class UpdateTournamentCommandHandler : IRequestHandler<UpdateTournamentCommand, ApiResponse<bool>>
    {
        private readonly ITournamentServices _tournamentServices;
        private readonly IMapper _mapper;

        public UpdateTournamentCommandHandler(ITournamentServices tournamentServices, IMapper mapper)
        {
            _tournamentServices = tournamentServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> Handle(UpdateTournamentCommand request, CancellationToken cancellationToken)
        {
            var tournament = _mapper.Map<Tournament>(request.UpdateTournamentDTO);

            var result = await _tournamentServices.UpdateTournamentAsync(tournament);

            return ApiResponseHandler.Build(
                data: result.Value,
                statusCode: result.StatusCode,
                succeeded: result.IsSuccess,
                message: result.IsSuccess ? "Tournament updated successfully" : result.Error.Message,
                errors: [result.Error.Message]
            );
        }
    }

}