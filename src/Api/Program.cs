public static class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder();
        var startup = new Startup(builder.Configuration);
        startup.ConfigureServices(builder.Services);
        var app = builder.Build();
        startup.Configure(app);
        app.Run();
    }
}