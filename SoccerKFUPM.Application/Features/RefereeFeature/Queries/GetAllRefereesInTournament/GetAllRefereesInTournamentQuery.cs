using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.RefereeDTOs;

namespace SoccerKFUPM.Application.Features.RefereeFeature.Queries.GetAllRefereesInTournament;

public record GetAllRefereesInTournamentQuery(int TournamentId) : IRequest<ApiResponse<List<TournamentRefereeViewDTO>>>;
