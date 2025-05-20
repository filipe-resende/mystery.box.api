namespace Api.Injections;

public static class DependencyRepositoryCollections
{
    public static IServiceCollection AddRepository(this IServiceCollection service)
    {
        service.AddScoped<ISteamCardRepository, SteamCardRepository>();
        service.AddScoped<IPaymentRepository, PaymentRepository>();
        service.AddScoped<ISteamCardCategoryRepository, SteamCardCatergoryRepository>();
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IPaymentGatewayService, MercadoPagoPaymentGatewayService>();
        service.AddScoped<IPurchaseHistoryRepository, PurchaseHistoryRepository>();
        service.AddScoped<IAuthenticationService, AuthenticationService>();
        service.AddScoped<IEmailService, EmailService>();

        service.AddHttpContextAccessor();

        return service;
    }
}

