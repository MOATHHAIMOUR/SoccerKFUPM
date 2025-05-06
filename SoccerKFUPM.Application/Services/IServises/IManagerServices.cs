using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.ManagerDTOs;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;

namespace SoccerKFUPM.Application.Services.IServises;

public interface IManagerServices
{
    Task<Result<bool>> AddManagerAsync(Manager manager);
    Task<Result<ManagerDTO>> GetManagerByIdAsync(int managerId);
    Task<Result<(List<ManagerView> Managers, int TotalCount)>> SearchManagersAsync(
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