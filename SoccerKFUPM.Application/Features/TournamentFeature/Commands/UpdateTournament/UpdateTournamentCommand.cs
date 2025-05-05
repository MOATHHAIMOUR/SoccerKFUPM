using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TournamentDTOs;

namespace SoccerKFUPM.Application.Features.TournamentFeature.Commands.UpdateTournament;

public record UpdateTournamentCommand(int Id, UpdateTournamentDTO UpdateTournamentDTO) : IRequest<ApiResponse<bool>>;