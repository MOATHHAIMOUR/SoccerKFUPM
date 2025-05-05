using AutoMapper;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.sharedDTOs;
using SoccerKFUPM.Application.DTOs.SharedDTOs;
using SoccerKFUPM.Application.Services.IServises;

namespace SoccerKFUPM.Application.Services;
public class SharedServices : ISharedServices
{
    private readonly ISharedRepository _sharedRepository;
    private readonly IMapper _mapper;

    public SharedServices(ISharedRepository sharedRepository, IMapper mapper)
    {
        _sharedRepository = sharedRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<CountryDTO>>> GetAllCountriesAsync()
    {
        var countries = await _sharedRepository.GetAllCountriesAsync();

        var countriesDTO = _mapper.Map<List<CountryDTO>>(countries);

        return Result<List<CountryDTO>>.Success(countriesDTO);

    }

    public async Task<Result<List<DepartmentDTO>>> GetAllDepartmentsAsync()
    {
        var departments = await _sharedRepository.GetAllDepartmentsAsync();
        var departmentDTOs = _mapper.Map<List<DepartmentDTO>>(departments);
        return Result<List<DepartmentDTO>>.Success(departmentDTOs);
    }


}