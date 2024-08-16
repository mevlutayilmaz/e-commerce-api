using ECommerceAPI.Application.Interfaces.Tokens;
using ECommerceAPI.Infrastructure.Services.Tokens;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection collection)
        {
            collection.AddScoped<ITokenHandler, TokenHandler>();
        }
    }
}
