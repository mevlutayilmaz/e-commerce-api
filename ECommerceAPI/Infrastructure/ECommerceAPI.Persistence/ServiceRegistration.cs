using ECommerceAPI.Application.Interfaces.Services;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories;
using ECommerceAPI.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddDbContext<ECommerceDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("MSSQL Server")));
            collection.AddIdentity<AppUser, AppRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ECommerceDbContext>();

            collection.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            collection.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            collection.AddScoped<IUserService, UserService>();
            collection.AddScoped<IAuthService, AuthService>();
            collection.AddScoped<IBasketService, BasketService>();
            collection.AddScoped<IOrderService, OrderService>();
        }
    }
}
