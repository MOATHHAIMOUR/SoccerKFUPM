using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.PlayerDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.PlayerFeature.Queries.PlayerViolations
{
    public class PlayerViolationsQueryHandler : IRequestHandler<PlayerViolationsQuery, ApiResponse<List<PlayerViolationDTO>>>
    {
        private readonly IPlayerServices _playerServices;
        private readonly IMapper _mapper;

        public PlayerViolationsQueryHandler(IPlayerServices playerServices, IMapper mapper)
        {
            _playerServices = playerServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<PlayerViolationDTO>>> Handle(PlayerViolationsQuery request, CancellationToken cancellationToken)
        {
            var result = await _playerServices.GetPlayerViolationsAsync(request.PageNumber, request.PageSize, request.CardType);

            var palyers = _mapper.Map<List<PlayerViolationDTO>>(result.Value);

            return ApiResponseHandler.Build(palyers, result.StatusCode, result.IsSuccess);
        }
    }
}
