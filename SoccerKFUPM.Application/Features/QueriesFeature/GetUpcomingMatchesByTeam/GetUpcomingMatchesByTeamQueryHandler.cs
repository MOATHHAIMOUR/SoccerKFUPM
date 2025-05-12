using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.MatchDTOs;
using SoccerKFUPM.Domain.IRepository;

namespace SoccerKFUPM.Application.Features.QueriesFeature.GetUpcomingMatchesByTeam
{
    public class GetUpcomingMatchesByTeamQueryHandler : IRequestHandler<GetUpcomingMatchesByTeamQuery, ApiResponse<IEnumerable<UpcomingMatchDTO>>>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;

        public GetUpcomingMatchesByTeamQueryHandler(IMatchRepository matchRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<UpcomingMatchDTO>>> Handle(GetUpcomingMatchesByTeamQuery request, CancellationToken cancellationToken)
        {
            var matches = await _matchRepository.GetUpcomingMatchesByTeamAsync(
                request.TeamName,
                request.TournamentName,
                request.FromDate,
                request.ToDate,
                request.PageNumber,
                request.PageSize);

            var mappedMatches = _mapper.Map<IEnumerable<UpcomingMatchDTO>>(matches);
            return ApiResponseHandler.Success(mappedMatches);
        }
    }
}
