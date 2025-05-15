
namespace Api.Injections;

public static class DependencyRepositoryCollections
{
    public static IServiceCollection AddRepository(this IServiceCollection service)
    {
        service.AddScoped<ISteamCardRepository, SteamCardRepository>();
        service.AddScoped<ISteamCardCategoryRepository, SteamCardCatergoryRepository>();
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IPaymentGatewayService, MercadoPagoPaymentGatewayService>();
        service.AddScoped<IAuthenticationService, AuthenticationService>();
        service.AddScoped<IEmailService, EmailService>();

        service.AddHttpContextAccessor();
        service.AddScoped<IPaymentGatewayService, MercadoPagoPaymentGatewayService>();
      

        return service;
    }
}

