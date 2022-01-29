using LibApp.Application.Core.Contracts.Persistence;
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

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IDataSeed, DataSeed>();
            services.AddScoped<IBookRespository, BookRespository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMembershipTypeRepository, MembershipTypeRepository>();

            return services;
        }
    }
}
