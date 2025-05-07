using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Features.TeamsFeature.Commands.AssignCoachIntoTeam;

public class AssignCoachIntoTeamCommandHandler : IRequestHandler<AssignCoachIntoTeamCommand, ApiResponse<bool>>
{
    private readonly ITeamServices _teamServices;
    private readonly IMapper _mapper;

    public AssignCoachIntoTeamCommandHandler(IMapper mapper, ITeamServices teamServices)
    {
        _mapper = mapper;
        _teamServices = teamServices;
    }

    public async Task<ApiResponse<bool>> Handle(AssignCoachIntoTeamCommand request, CancellationToken cancellationToken)
    {

        // Map and execute
        var coachTeam = _mapper.Map<CoachTeam>(request.AssignCoachIntoTeamDTO);
        var result = await _teamServices.AssignCoachToTeamAsync(coachTeam, request.AssignCoachIntoTeamDTO.TournamentId);

        return ApiResponseHandler.Build(
            data: result.Value,
            statusCode: result.StatusCode,
            succeeded: result.IsSuccess,
            message: result.IsSuccess ? "Coach assigned to team successfully" : result.Error.Message,
            errors: result.IsSuccess ? null : [result.Error.Message]
        );
    }
}