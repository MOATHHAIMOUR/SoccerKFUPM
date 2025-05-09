using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;

namespace SoccerKFUPM.Domain.IRepository;

public interface IManagerRepository
{
    Task<int?> AddManagerAsync(Manager manager);
    Task<ManagerView?> GetManagerByIdAsync(int managerId);
    public Task<(List<ManagerView> managers, int totalCount)> SearchManagersAsync(
            string? kfupmId = null,
            string? firstName = null,
            string? secondName = null,
            string? thirdName = null,
            string? lastName = null,
            DateTime? dateOfBirth = null,
            int? nationalityId = null,
            string? teamName = null,
            int pageNumber = 1,
            int pageSize = 10);
}