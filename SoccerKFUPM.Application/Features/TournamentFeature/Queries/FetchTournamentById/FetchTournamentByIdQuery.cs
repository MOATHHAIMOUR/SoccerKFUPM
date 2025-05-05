using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TournamentDTOs;

namespace SoccerKFUPM.Application.Features.TournamentFeature.Queries.FetchTournamentById;

public record FetchTournamentByIdQuery(int TournamentId) : IRequest<ApiResponse<TournamentDTO>>;