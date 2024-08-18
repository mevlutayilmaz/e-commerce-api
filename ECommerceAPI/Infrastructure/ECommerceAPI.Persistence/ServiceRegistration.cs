using ECommerceAPI.Application.Interfaces.Services;
using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Identity;
using ECommerceAPI.Persistence.Contexts;
using ECommerceAPI.Persistence.Repositories;
using ECommerceAPI.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ECommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddDbContext<ECommerceDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("MSSQL Server")));
            collection.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ECommerceDbContext>()
            .AddDefaultTokenProviders();

            collection.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            collection.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            collection.AddScoped<IUserService, UserService>();
            collection.AddScoped<IAuthService, AuthService>();
            collection.AddScoped<IBasketService, BasketService>();
            collection.AddScoped<IOrderService, OrderService>();
        }
    }
}
