using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Features.CoachFeature.Commands.AddCoach;

public class AddCoachCommandHandler : IRequestHandler<AddCoachCommand, ApiResponse<bool>>
{
    private readonly ICoachServices _coachServices;
    private readonly IMapper _mapper;

    public AddCoachCommandHandler(ICoachServices coachServices, IMapper mapper)
    {
        _coachServices = coachServices;
        _mapper = mapper;
    }

    public async Task<ApiResponse<bool>> Handle(AddCoachCommand request, CancellationToken cancellationToken)
    {
        // Map and execute
        var coach = _mapper.Map<Coache>(request.Dto);
        var result = await _coachServices.AddCoachAsync(coach);

        return ApiResponseHandler.Build(
            data: result.Value,
            statusCode: result.StatusCode,
            succeeded: result.IsSuccess,
            message: result.IsSuccess ? "Coach added successfully" : result.Error.Message,
            errors: result.IsSuccess ? null : [result.Error.Message]
        );
    }
}