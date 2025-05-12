using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Features.RefereeFeature.Commands.AddNewReferee;

public class AddNewRefereeCommandHandler : IRequestHandler<AddNewRefereeCommand, ApiResponse<bool>>
{
    private readonly IRefereeServices _refereeServices;
    private readonly IMapper _mapper;

    public AddNewRefereeCommandHandler(IRefereeServices refereeServices, IMapper mapper)
    {
        _refereeServices = refereeServices;
        _mapper = mapper;
    }

    public async Task<ApiResponse<bool>> Handle(AddNewRefereeCommand request, CancellationToken cancellationToken)
    {
        var referee = _mapper.Map<Referee>(request.RefereeDTO);
        var result = await _refereeServices.AddRefereeAsync(referee, request.RefereeDTO.Username, request.RefereeDTO.IntialPassword);
        return ApiResponseHandler.Build(result.Value, result.StatusCode, result.IsSuccess);
    }
}
