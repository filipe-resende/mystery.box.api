namespace Api.Injections;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<MercadoPagoSettings>(configuration.GetSection("MercadoPago"));

        service.AddScoped<PaymentClient>();

        service.AddHttpClient<MercadoPagoPaymentGatewayService>(client =>
        {
            client.BaseAddress = new Uri("https://api.mercadopago.com/");
        });

        service.AddScoped<IPaymentGatewayService, MercadoPagoPaymentGatewayService>();

        return service;
    }
}



