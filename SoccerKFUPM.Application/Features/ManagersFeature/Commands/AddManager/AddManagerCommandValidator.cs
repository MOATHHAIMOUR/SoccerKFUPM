using FluentValidation;
using SoccerKFUPM.Application.DTOs.ContactInfoDTOs;
using SoccerKFUPM.Application.DTOs.ManagerDTOs;

namespace SoccerKFUPM.Application.Features.ManagersFeature.Commands.AddManager;

public class AddManagerCommandValidator : AbstractValidator<AddManagerDTO>
{
    public AddManagerCommandValidator()
    {
        RuleFor(x => x.KFUPMId)
            .NotEmpty().WithMessage("KFUPM ID is required")
            .Length(9).WithMessage("KFUPM ID must be exactly 9 characters");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters");

        RuleFor(x => x.SecondName)
            .MaximumLength(50).WithMessage("Second name must not exceed 50 characters")
            .When(x => !string.IsNullOrEmpty(x.SecondName));

        RuleFor(x => x.ThirdName)
            .MaximumLength(50).WithMessage("Third name must not exceed 50 characters")
            .When(x => !string.IsNullOrEmpty(x.ThirdName));

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required")
            .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past");

        RuleFor(x => x.PersonalContactInfos)
            .NotEmpty().WithMessage("At least one contact information is required")
            .Must(x => x.Count <= 5).WithMessage("Maximum 5 contact information entries allowed");

        RuleForEach(x => x.PersonalContactInfos).SetValidator(new PersonalContactInfoValidator());
    }
}