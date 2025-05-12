using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.ManagerDTOs;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;

namespace SoccerKFUPM.Application.Services.IServises;

public interface IManagerServices
{
    public Task<Result<bool>> AddManagerAsync(Manager manager, string username, string IntialPassword);
    Task<Result<ManagerViewDTO>> GetManagerByIdAsync(int managerId);
    Task<Result<(List<ManagerSearchViewDTO> Managers, int TotalCount)>> SearchManagersAsync(
       string? kfupmId,
       string? firstName,
       string? secondName,
       string? thirdName,
       string? lastName,
       DateTime? dateOfBirth,
       int? nationalityId,
       string? teamName,
       int pageNumber,
       int pageSize);
}