using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Features.RefereeFeature.Commands.AssignRefereeToTournament;

public class AssignRefereeToTournamentCommandHandler : IRequestHandler<AssignRefereeToTournamentCommand, ApiResponse<bool>>
{
    private readonly IRefereeServices _refereeServices;

    public AssignRefereeToTournamentCommandHandler(IRefereeServices refereeServices)
    {
        _refereeServices = refereeServices;
    }

    public async Task<ApiResponse<bool>> Handle(AssignRefereeToTournamentCommand request, CancellationToken cancellationToken)
    {
        var result = await _refereeServices.AssignRefereeToTournamentAsync(request.RefereeId, request.TournamentId);
        return ApiResponseHandler.Build(result.Value, result.StatusCode, result.IsSuccess);
    }
}
