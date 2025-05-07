using FluentValidation;

namespace SoccerKFUPM.Application.Features.CoachFeature.Commands.AddCoach;

public class AddCoachDTOValidator : AbstractValidator<AddCoachCommand>
{
    public AddCoachDTOValidator()
    {
        RuleFor(x => x.Dto.KFUPMId)
            .NotEmpty().WithMessage("KFUPM ID is required")
            .Length(9).WithMessage("KFUPM ID must be exactly 9 characters");

        RuleFor(x => x.Dto.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

        RuleFor(x => x.Dto.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");

        RuleFor(x => x.Dto.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required")
            .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past");

        RuleFor(x => x.Dto.PersonalContactInfos)
            .NotEmpty().WithMessage("At least one contact information is required")
            .Must(x => x.Count <= 5).WithMessage("Cannot have more than 5 contact information entries");
    }
}