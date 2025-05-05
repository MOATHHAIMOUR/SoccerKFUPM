using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.sharedDTOs;
using SoccerKFUPM.Application.DTOs.SharedDTOs;

namespace SoccerKFUPM.Application.Services.IServises;
public interface ISharedServices
{
    Task<Result<List<CountryDTO>>> GetAllCountriesAsync();
    Task<Result<List<DepartmentDTO>>> GetAllDepartmentsAsync();
}