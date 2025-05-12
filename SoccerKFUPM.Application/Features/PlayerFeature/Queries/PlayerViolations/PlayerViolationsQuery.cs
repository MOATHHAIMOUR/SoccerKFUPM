using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.PlayerDTOs;

namespace SoccerKFUPM.Application.Features.PlayerFeature.Queries.PlayerViolations
{
    public record PlayerViolationsQuery(int PageNumber = 1, int PageSize = 10, int? CardType = null) : IRequest<ApiResponse<List<PlayerViolationDTO>>>;
}
