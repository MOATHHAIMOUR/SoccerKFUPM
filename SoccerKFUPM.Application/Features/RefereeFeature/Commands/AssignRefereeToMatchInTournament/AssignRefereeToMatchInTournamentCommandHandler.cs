using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.RefereeFeature.Commands.AssignRefereeToMatchInTournament;

public class AssignRefereeToMatchInTournamentCommandHandler : IRequestHandler<AssignRefereeToMatchInTournamentCommand, ApiResponse<bool>>
{
    private readonly IRefereeServices _refereeServices;

    public AssignRefereeToMatchInTournamentCommandHandler(IRefereeServices refereeServices)
    {
        _refereeServices = refereeServices;
    }

    public async Task<ApiResponse<bool>> Handle(AssignRefereeToMatchInTournamentCommand request, CancellationToken cancellationToken)
    {
        var result = await _refereeServices.AssignRefereeToMatchAsync(request.MatchScheduleId, request.tournamentRefereeId);
        return ApiResponseHandler.Build(result.Value, result.StatusCode, result.IsSuccess);
    }
}
