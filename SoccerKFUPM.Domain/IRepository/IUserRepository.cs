using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Domain.IRepository;

public interface IUserRepository
{

    public Task<User?> GetUserByIdOrUserName(int? UserId, string? UserName);

}