using LibApp.Application.Core.Contracts.Persistence;
using LibApp.Domain.Entities;
using LibApp.Persistence.Repositories;
using LibApp.Persistence.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Persistence
{
    public static class RegisterServices
    {
        public static IServiceCollection ReisterPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDataSeed, DataSeed>();
            services.AddScoped<IBookRespository, BookRespository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMembershipTypeRepository, MembershipTypeRepository>();

            services.AddIdentity<Customer, IdentityRole<int>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
