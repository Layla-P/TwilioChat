using Microsoft.Extensions.DependencyInjection;

namespace TwilioChat.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceServices(this IServiceCollection services)
        {
            RegisterRepositoryServices(services);
            return services;
        }

        private static void RegisterRepositoryServices(IServiceCollection services)
        {
           services.AddScoped<ITokenGenerator, TokenGenerator>();
        }
    }
}
