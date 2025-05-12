using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.PlayerDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.PlayerFeature.Queries.TopScorerPlayer
{
    public class TopScorerPlayerQueryHandler : IRequestHandler<TopScorerPlayerQuery, ApiResponse<List<TopScorerPlayerDTO>>>
    {
        private readonly IPlayerServices _playerServices;
        private readonly IMapper _mapper;

        public TopScorerPlayerQueryHandler(IPlayerServices playerServices, IMapper mapper)
        {
            _playerServices = playerServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<TopScorerPlayerDTO>>> Handle(TopScorerPlayerQuery request, CancellationToken cancellationToken)
        {
            var result = await _playerServices.GetTopScorersAsync(request.PageNumber, request.PageSize);
            var playerDTO = _mapper.Map<List<TopScorerPlayerDTO>>(result.Value);
            return ApiResponseHandler.Build(playerDTO, result.StatusCode, result.IsSuccess);
        }
    }
}
