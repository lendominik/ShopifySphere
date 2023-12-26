using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Interfaces;
using Shop.Infrastructure.Persistence;
using Shop.Infrastructure.Repositories;

namespace Shop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShopDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("ShopDbContext")));

            services.AddHttpContextAccessor();

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ShopDbContext>();

            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICartItemRepository, CartItemRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
        }
    }
}
