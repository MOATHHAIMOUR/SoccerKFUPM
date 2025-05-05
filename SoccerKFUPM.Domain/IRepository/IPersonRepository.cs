using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Infrastructure.Repository
{
    public interface IPersonRepository
    {
        public Task<int> AddPersonAsync(Person person);

        public Task<bool> CheckIsPersonExistAsync(string KFUPMId);
    }
}
