using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TournamentDTOs;

namespace SoccerKFUPM.Application.Features.TournamentFeature.Commands.AddTournament;

public record AddTournamentCommand(AddTournamentDTO AddTournamentDTO) : IRequest<ApiResponse<bool>>;