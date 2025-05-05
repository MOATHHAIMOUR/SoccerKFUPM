using MediatR;
using SoccerKFUPM.Application.DTOs.PlayerDTOs;
using SoccerKFUPM.Application.Common.ResultPattern;

namespace SoccerKFUPM.Application.Features.PlayerFeature.Queries.FetchPlayerById
{
    public record FetchPlayerByIdQuery(int PlayerId) : IRequest<ApiResponse<PlayerDTO>>;
}