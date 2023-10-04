using Application.DBContext;

namespace Application.Cross.DependencyInjections
{
    public static class DependencyInjections
    {
        public static void RegisterDependencies(IServiceCollection sevice)
        {
            sevice.AddScoped<Context>();
        }
    }
}
