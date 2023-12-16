using Application.DBContext;
using Application.Repository;
using Application.UseCase;
using Domain;
using Domain.Interfaces.Repositories;
using Infraestructure.Services;

namespace Application.Configurations
{
    public static class DependencyRepositoryCollections
    {
        public static IServiceCollection AddRepository(this IServiceCollection service)
        {
            service.AddScoped<Context>();
            service.AddScoped<ISteamCardRepository, SteamCardRepository>();
            service.AddScoped<ISteamCardCategoryRepository, SteamCardCatergoryRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<ICheckoutRepository, CheckoutRepository>();
            service.AddScoped<IGetUserFromToken, GetUserFromToken>();
            service.AddScoped<IAuthenticationService, AuthenticationService>();
            service.AddScoped<IEmailService, EmailService>();

            service.AddHttpContextAccessor();
            service.AddHttpClient<ICheckoutRepository, CheckoutRepository>(client =>
            {
                string urlBase = Environment.GetEnvironmentVariable("Checkout_Base_Url");
                client.BaseAddress = new Uri(urlBase);
            });

            return service;
        }
    }
}
