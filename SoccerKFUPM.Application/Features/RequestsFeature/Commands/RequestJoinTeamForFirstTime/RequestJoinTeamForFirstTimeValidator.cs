using FluentValidation;
using SoccerKFUPM.Application.DTOs.RequestDTOs;

namespace SoccerKFUPM.Application.Features.RequestsFeature.Commands.RequestJoinTeamForFirstTime;

public class RequestJoinTeamForFirstTimeValidator : AbstractValidator<RequestJoinTeamDTO>
{
    public RequestJoinTeamForFirstTimeValidator()
    {
        RuleFor(x => x.PlayerId)
            .GreaterThan(0).WithMessage("Player ID must be greater than 0");

        RuleFor(x => x.TeamId)
            .GreaterThan(0).WithMessage("Team ID must be greater than 0");

        RuleFor(x => x.PreferredPosition)
            .IsInEnum().WithMessage("Invalid player position");
    }
}