using Microsoft.Extensions.DependencyInjection;
using Namespace.SoccerKFUPM.Domain.IRepository;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.IRepository;
using SoccerKFUPM.Infrastructure.Repository;

namespace SoccerKFUPM.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services)
        {

            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISharedRepository, SharedRepossitory>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            return services;
        }
    }

}
