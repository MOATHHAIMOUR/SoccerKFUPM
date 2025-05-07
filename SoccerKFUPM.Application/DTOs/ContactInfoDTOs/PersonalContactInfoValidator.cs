using FluentValidation;
using SoccerKFUPM.Domain.Entities.Enums;

namespace SoccerKFUPM.Application.DTOs.ContactInfoDTOs;

public class PersonalContactInfoValidator : AbstractValidator<PersonalContactInfoDTO>
{
    public PersonalContactInfoValidator()
    {
        RuleFor(x => x.ContactType)
            .IsInEnum().WithMessage("Invalid contact type");

        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Contact value is required")
            .MaximumLength(100).WithMessage("Contact value must not exceed 100 characters")
            .Must((dto, value) => IsValidContactValue((ContactType)dto.ContactType, value))
            .WithMessage("Invalid contact value format for the specified contact type");
    }

    private bool IsValidContactValue(ContactType type, string value)
    {
        return type switch
        {
            ContactType.Email => value.Contains("@") && value.Contains("."),
            ContactType.Phone => value.All(c => char.IsDigit(c) || c == '+' || c == '-'),
            ContactType.WhatsApp => value.All(c => char.IsDigit(c) || c == '+'),
            _ => true
        };
    }
}