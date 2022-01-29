using LibApp.Application.Core.Profiles.ValueResolvers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace LibApp.Application
{
    public static class RegisterServices
    {
        public static IServiceCollection ReisterApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddHttpClient("APIClient", client =>
            {
                client.BaseAddress = new Uri(configuration["API:Address"]);
            });

            services.AddTransient<CreateBookGenreValueResolver>();
            services.AddTransient<UpdateBookGenreValueResolver>();
            services.AddTransient<CreateCustomerMembershipTypeGenreValueResolver>();
            services.AddTransient<UpdateCustomerMembershipTypeGenreValueResolver>();

            return services;
        }
    }
}
