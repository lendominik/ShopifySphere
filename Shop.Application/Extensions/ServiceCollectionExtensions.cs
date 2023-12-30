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
using Shop.Application.Services;
using Stripe;
using Shop.Application.Services.ItemServices;
using Shop.Application.Services.OrderServices;
using Shop.Application.Services.CartServices;

namespace Shop.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAplication(this IServiceCollection services)
        {
            services.AddTransient<IUserContext, UserContext>();
            services.AddMediatR(typeof(GetAllItemsQuery));
            services.AddMediatR(typeof(GetCartQuery));

            services.AddMediatR(typeof(CreateOrderCommand));

            services.AddHttpContextAccessor();

            services.AddScoped<ErrorHandlingMiddleware>();

            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IAccessControlService, AccessControlService>();
            services.AddTransient<IFileService, Services.FileService>();
            services.AddTransient<IItemFilterService, ItemFilterService>();
            services.AddTransient<IOrderFilterService, OrderFilterService>();
            services.AddTransient<IPaginationService, PaginationService>();
            services.AddTransient<IItemSortService, ItemSortService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<ICartIdProviderService, CartIdProviderService>();
            services.AddTransient<ICartCalculatorService, CartCalculatorService>();
            services.AddTransient<ICartRepositoryService, CartRepositoryService>();
            services.AddTransient<ICartUpdaterService, CartUpdaterService>();

            services.AddValidatorsFromAssemblyContaining<CreateItemCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddTransient(provider =>
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
