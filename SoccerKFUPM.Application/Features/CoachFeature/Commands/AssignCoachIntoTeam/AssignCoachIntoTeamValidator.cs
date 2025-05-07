using FluentValidation;
using SoccerKFUPM.Application.DTOs.TeamDTOs;

namespace SoccerKFUPM.Application.Features.CoachFeature.Commands.AssignCoachIntoTeam;

public class AssignCoachIntoTeamValidator : AbstractValidator<AssignCoachIntoTeamDTO>
{
    public AssignCoachIntoTeamValidator()
    {
        RuleFor(x => x.TeamId)
            .GreaterThan(0)
            .WithMessage("TeamId must be greater than 0");

        RuleFor(x => x.CoachId)
            .GreaterThan(0)
            .WithMessage("CoachId must be greater than 0");
    }
}