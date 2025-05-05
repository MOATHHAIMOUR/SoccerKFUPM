using SoccerKFUPM.Application.DTOs.AuthDTOs;
using SoccerKFUPM.Application.DTOs.sharedDTOs;
using SoccerKFUPM.Domain.Entities;
namespace SoccerKFUPM.Application.DTOs.SharedDTOs.Profile;

public class DepartmentsProfile : AutoMapper.Profile
{
    public DepartmentsProfile()
    {
        CreateMap<Country, CountryDTO>();
        CreateMap<Role, RoleDTO>();

        CreateMap<Department, DepartmentDTO>();
    }
}