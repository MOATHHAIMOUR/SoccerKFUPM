using FluentValidation;
using SoccerKFUPM.Application.DTOs.TeamDTOs;

namespace SoccerKFUPM.Application.Features.TeamsFeature.Commands.UpdateTeam;

public class UpdateTeamDTOValidator : AbstractValidator<UpdateTeamDTO>
{
    public UpdateTeamDTOValidator()
    {
        RuleFor(x => x.TeamId)
            .GreaterThan(0).WithMessage("Team ID must be greater than 0");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Team name is required")
            .MaximumLength(100).WithMessage("Team name must not exceed 100 characters");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required")
            .MaximumLength(200).WithMessage("Address must not exceed 200 characters");

        RuleFor(x => x.Website)
            .NotEmpty().WithMessage("Website is required")
            .MaximumLength(100).WithMessage("Website must not exceed 100 characters")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Website must be a valid URL");

        RuleFor(x => x.NumberOfPlayers)
            .GreaterThan(0).WithMessage("Number of players must be greater than 0")
            .LessThanOrEqualTo(30).WithMessage("Number of players must not exceed 30");

    }
}