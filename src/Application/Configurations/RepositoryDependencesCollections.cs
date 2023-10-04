using Application.DBContext;
using Application.Repository;
using Domain.Interfaces.Repositories;

namespace Application.Configurations
{
    public static class DependencyRepositoryCollections
    {
        public static IServiceCollection AddRepository(this IServiceCollection service)
        {
            service.AddScoped<Context>();
            service.AddScoped<ISteamCardRepository, SteamCardRepository>();
            service.AddScoped<IUserRepository, UserRepository>();

            return service;
        }
    }
}
