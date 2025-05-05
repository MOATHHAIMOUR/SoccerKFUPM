using SoccerKFUPM.Domain.Entities;
namespace SoccerKFUPM.Application.Services.IServises;
public interface ISharedRepository
{
    Task<List<Country>> GetAllCountriesAsync();
    Task<List<Department>> GetAllDepartmentsAsync();
}