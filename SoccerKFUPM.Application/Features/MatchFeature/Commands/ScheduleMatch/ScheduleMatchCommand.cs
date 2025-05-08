using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Domain.Entities.Enums;

namespace SoccerKFUPM.Application.Features.MatchFeature.Commands.ScheduleMatch;

public record ScheduleMatchCommand(
    int TournamentId,
    TournamentPhase TournamentPhase,
    int TournamentTeamIdA,
    int TournamentTeamIdB,
    DateTime Date,
    int FieldId
) : IRequest<ApiResponse<bool>>;