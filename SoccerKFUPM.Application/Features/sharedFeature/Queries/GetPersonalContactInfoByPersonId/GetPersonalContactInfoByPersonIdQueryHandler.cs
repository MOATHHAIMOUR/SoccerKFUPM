using AutoMapper;
using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Common.Errors;
using SoccerKFUPM.Application.DTOs.ContactInfoDTOs;
using SoccerKFUPM.Application.Services.IServises;
using System.Net;

namespace SoccerKFUPM.Application.Features.SharedFeature.Queries.GetPersonalContactInfoByPersonId;

public class GetPersonalContactInfoByPersonIdQueryHandler : IRequestHandler<GetPersonalContactInfoByPersonIdQuery, Result<List<ContactInfoDTO>>>
{
    private readonly ISharedServices _sharedServices;
    private readonly IMapper _mapper;

    public GetPersonalContactInfoByPersonIdQueryHandler(ISharedServices sharedServices, IMapper mapper)
    {
        _sharedServices = sharedServices;
        _mapper = mapper;
    }

    public async Task<Result<List<ContactInfoDTO>>> Handle(GetPersonalContactInfoByPersonIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _sharedServices.GetPersonalContactInfoByPersonIdAsync(request.PersonId);
        
        if (!result.IsSuccess)
            return Result<List<ContactInfoDTO>>.Failure(result.Error, result.StatusCode);

        var contactInfoDTOs = _mapper.Map<List<ContactInfoDTO>>(result.Value);
        return Result<List<ContactInfoDTO>>.Success(contactInfoDTOs);
    }
}
