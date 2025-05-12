using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.PlayerDTOs;

namespace SoccerKFUPM.Application.Features.PlayerFeature.Queries.TopScorerPlayer
{
    public record TopScorerPlayerQuery(int PageNumber = 1, int PageSize = 10) : IRequest<ApiResponse<List<TopScorerPlayerDTO>>>;
}
