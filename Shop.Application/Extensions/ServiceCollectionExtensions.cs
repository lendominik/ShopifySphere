using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.ApplicationUser;
using Shop.Application.Cart.Queries.GetCart;
using Shop.Application.Item.Commands.CreateItem;
using Shop.Application.Item.Queries.GetAllItems;
using Shop.Application.Mappings;
using Shop.Application.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Shop.Application.Category.Commands.CreateCategory;
using Shop.Application.Order.Commands.CreateOrder;

namespace Shop.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
            services.AddMediatR(typeof(GetAllItemsQuery));
            services.AddMediatR(typeof(GetCartQuery));

            services.AddMediatR(typeof(CreateOrderCommand));

            services.AddHttpContextAccessor();

            services.AddTransient<ErrorHandlingMiddleware>();

            services.AddFluentValidation();
            services.AddValidatorsFromAssemblyContaining<CreateItemCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddScoped(provider =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new ShopMappingProfile());
                });

                return config.CreateMapper();
            });
        }
    }
}
