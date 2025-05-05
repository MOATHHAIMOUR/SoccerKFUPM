using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Domain.IRepository
{
    public interface IRefreshTokenRepository
    {
        public Task<bool> AddRefreshTokenAsync(RefreshToken refreshToken);
        public Task<(User? user, bool result)> CheckRefreshTokenIsValidAsync(string refreshToken);

    }
}
