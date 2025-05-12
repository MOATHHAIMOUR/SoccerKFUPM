using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.RefereeDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.RefereeFeature.Queries.GetAllRefereesInTournament;

public class GetAllRefereesInTournamentQueryHandler
    : IRequestHandler<GetAllRefereesInTournamentQuery, ApiResponse<List<TournamentRefereeViewDTO>>>
{
    private readonly IRefereeServices _refereeServices;
    private readonly ITournamentServices _tournamentServices;
    private readonly IMapper _mapper;

    public GetAllRefereesInTournamentQueryHandler(
        IRefereeServices refereeServices,
        ITournamentServices tournamentServices,
        IMapper mapper)
    {
        _refereeServices = refereeServices;
        _tournamentServices = tournamentServices;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<TournamentRefereeViewDTO>>> Handle(
        GetAllRefereesInTournamentQuery request,
        CancellationToken cancellationToken)
    {

        // Get referees
        var refereesResult = await _refereeServices.GetRefereesInTournamentAsync(request.TournamentId);

        return ApiResponseHandler.Build(
            refereesResult.Value,
            refereesResult.StatusCode,
            refereesResult.IsSuccess,
            null,
            [refereesResult.Error.Message]
        );

    }
}
