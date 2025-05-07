using MediatR;
using SoccerKFUPM.Application.Common.ApiResponse;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.SharedDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Features.FieldFeature.Queries.GetAllFields;

public class GetAllFieldsQueryHandler : IRequestHandler<GetAllFieldsQuery, ApiResponse<List<FieldDTO>>>
{
    private readonly IFieldServices _fieldServices;

    public GetAllFieldsQueryHandler(IFieldServices fieldServices)
    {
        _fieldServices = fieldServices;
    }

    public async Task<ApiResponse<List<FieldDTO>>> Handle(GetAllFieldsQuery request, CancellationToken cancellationToken)
    {
        var result = await _fieldServices.GetAllFieldsAsync();


        return ApiResponseHandler.Build<List<FieldDTO>>(result.Value, result.StatusCode, result.IsSuccess);
    }
}