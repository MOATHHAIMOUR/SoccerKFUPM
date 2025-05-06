using FluentValidation;

namespace SoccerKFUPM.Application.Features.TournamentFeature.Commands.AssignTeamsInTournament;

public class AssignTeamsInTournamentCommandValidator : AbstractValidator<AssignTeamsInTournamentCommand>
{
    public AssignTeamsInTournamentCommandValidator()
    {
        RuleFor(x => x.AssignTeamsInTournamentDTO.TournamentId)
            .NotEmpty()
            .WithMessage("Tournament ID is required");
        RuleFor(x => x.AssignTeamsInTournamentDTO.TeamIds)
            .NotEmpty().WithMessage("At least one team must be assigned.")
            .Must(x => x.Count > 0).WithMessage("At least one team must be assigned.")
            .Must(x => x.Distinct().Count() == x.Count)
            .WithMessage("Duplicate team assignments are not allowed.");
    }
}