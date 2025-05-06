using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Features.ManagersFeature.Commands.AddManager;

public class AddManagerCommandHandler : IRequestHandler<AddManagerCommand, ApiResponse<bool>>
{
    private readonly IManagerServices _managerServices;
    private readonly IMapper _mapper;

    public AddManagerCommandHandler(IManagerServices managerServices, IMapper mapper)
    {
        _managerServices = managerServices;
        _mapper = mapper;
    }

    public async Task<ApiResponse<bool>> Handle(AddManagerCommand request, CancellationToken cancellationToken)
    {
        var manager = _mapper.Map<Manager>(request.AddManagerDTO);
        var result = await _managerServices.AddManagerAsync(manager);
        return ApiResponseHandler.Build(result.Value, result.StatusCode, result.IsSuccess, null, [result.Error.Message]);
    }
}