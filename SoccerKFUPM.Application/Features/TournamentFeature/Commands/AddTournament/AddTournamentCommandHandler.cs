using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Features.TournamentFeature.Commands.AddTournament;

public class AddTournamentCommandHandler : IRequestHandler<AddTournamentCommand, ApiResponse<bool>>
{
    private readonly ITournamentServices _tournamentServices;
    private readonly IMapper _mapper;

    public AddTournamentCommandHandler(ITournamentServices tournamentServices, IMapper mapper)
    {
        _tournamentServices = tournamentServices;
        _mapper = mapper;
    }

    public async Task<ApiResponse<bool>> Handle(AddTournamentCommand request, CancellationToken cancellationToken)
    {
        var tournament = _mapper.Map<Tournament>(request.AddTournamentDTO);
        var result = await _tournamentServices.AddTournamentAsync(tournament);

        return ApiResponseHandler.Build(
            data: result.Value,
            statusCode: result.StatusCode,
            succeeded: result.IsSuccess,
            message: result.IsSuccess ? "Tournament created successfully" : result.Error.Message,
            errors: result.IsSuccess ? null : [result.Error.Message]
        );
    }
}