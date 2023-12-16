using Application.Cross.DependencyInjections.Filter;

namespace Application.Cross.DependencyInjections
{
    public static class ConflitExceptionExtensions
    {
        public static IServiceCollection AddConflitExceptionFilter(this IServiceCollection service) 
        {
            service.AddMvc(option =>
            {
                option.Filters.Add(typeof(UserAlreadyExistFilter));
                option.Filters.Add(typeof(UserNotFoundFilter));
            });

            return service;
        }
    }
}
