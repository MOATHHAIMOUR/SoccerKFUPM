using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Features.MatchFeature.Commands.AddMatchResult;

public class AddMatchResultCommandHandler : IRequestHandler<AddMatchResultCommand, ApiResponse<bool>>
{
    private readonly IMatchServices _matchServices;
    private readonly IMapper _mapper;

    public AddMatchResultCommandHandler(IMatchServices matchServices, IMapper mapper)
    {
        _matchServices = matchServices;
        _mapper = mapper;
    }

    public async Task<ApiResponse<bool>> Handle(AddMatchResultCommand request, CancellationToken cancellationToken)
    {
        // Map DTOs to domain entities
        var result = _mapper.Map<MatchRecord>(request.Dto);

        // Call service to record the match result
        var serviceResult = await _matchServices.AddMatchResultAsync(result);

        return ApiResponseHandler.Build(
            data: serviceResult.Value,
            statusCode: serviceResult.StatusCode,
            succeeded: serviceResult.IsSuccess,
            message: serviceResult.IsSuccess ? "Match result recorded successfully" : serviceResult.Error.Message,
            errors: serviceResult.IsSuccess ? null : [serviceResult.Error.Message]
        );
    }
}