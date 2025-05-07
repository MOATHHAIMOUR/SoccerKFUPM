using AutoMapper;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.SharedDTOs;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.IRepository;

namespace SoccerKFUPM.Application.Services;

public class FieldServices : IFieldServices
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IMapper _mapper;

    public FieldServices(IFieldRepository fieldRepository, IMapper mapper)
    {
        _fieldRepository = fieldRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<FieldDTO>>> GetAllFieldsAsync()
    {
        var fields = await _fieldRepository.GetAllFieldsAsync();
        var fieldDTOs = _mapper.Map<List<FieldDTO>>(fields);
        return Result<List<FieldDTO>>.Success(fieldDTOs);
    }
}