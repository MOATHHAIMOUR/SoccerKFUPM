using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.PlayerDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.PlayerFeature.Queries.FetchPlayerById
{
    public class FetchPlayerByIdQueryHandler : IRequestHandler<FetchPlayerByIdQuery, ApiResponse<PlayerDTO>>
    {
        private readonly IPlayerServices _playerServices;
        private readonly IMapper _mapper;

        public FetchPlayerByIdQueryHandler(IPlayerServices playerServices, IMapper mapper)
        {
            _playerServices = playerServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PlayerDTO>> Handle(FetchPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            var player = await _playerServices.GetPlayerByIdAsync(request.PlayerId);
            
            return ApiResponseHandler.Build(player.Value,player.StatusCode,player.IsSuccess);
        }
    }
}