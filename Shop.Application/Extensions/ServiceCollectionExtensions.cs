using Microsoft.Extensions.DependencyInjection;
using Shop.Application.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();
        }
    }
}
