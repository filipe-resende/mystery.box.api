
namespace Api.Injections;

public static class DependencyRepositoryCollections
{
    public static IServiceCollection AddRepository(this IServiceCollection service)
    {
        service.AddScoped<ISteamCardRepository, SteamCardRepository>();
        service.AddScoped<ISteamCardCategoryRepository, SteamCardCatergoryRepository>();
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<ICheckoutService, CheckoutService>();
        service.AddScoped<IGetUserFromToken, GetUserFromToken>();
        service.AddScoped<IAuthenticationService, AuthenticationService>();
        service.AddScoped<IEmailService, EmailService>();

        service.AddHttpContextAccessor();
        service.AddHttpClient<ICheckoutService, CheckoutService>(client =>
        {
            string urlBase = Environment.GetEnvironmentVariable("Checkout_Base_Url");
            client.BaseAddress = new Uri(urlBase);
        });

        return service;
    }
}

