using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.sharedDTOs;
using SoccerKFUPM.Application.DTOs.SharedDTOs;
using SoccerKFUPM.Application.DTOs.ContactInfoDTOs;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Services.IServises;
public interface ISharedServices
{
    Task<Result<List<CountryDTO>>> GetAllCountriesAsync();
    Task<Result<List<DepartmentDTO>>> GetAllDepartmentsAsync();
    Task<Result<List<PersonalContactInfo>>> GetPersonalContactInfoByPersonIdAsync(int personId);
}