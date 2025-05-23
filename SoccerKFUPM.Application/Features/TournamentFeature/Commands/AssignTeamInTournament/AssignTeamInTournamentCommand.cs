using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TournamentDTOs;

namespace SoccerKFUPM.Application.Features.TournamentFeature.Commands.AssignTeamsInTournament;

public record AssignTeamInTournamentCommand(AssignTeamInTournamentDTO AssignTeamsInTournamentDTO) : IRequest<ApiResponse<bool>>;