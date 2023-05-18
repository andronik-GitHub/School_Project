using Application.Common.Interfaces;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentitySchoolLibraryDbContext>(options =>
            {
                options
                    .UseSqlServer(
                        configuration.GetConnectionString("sqlConnection"),
                        b => b.MigrationsAssembly(typeof(IdentitySchoolLibraryDbContext).Assembly.GetName().Name)
                    )
                    .EnableSensitiveDataLogging();
            });
            
            services
                // Register ASP.NET Core Identity with method AddIdentity<TUser, TRole>
                .AddIdentity<UserIdentity, IdentityRole<Guid>>()
                // To register the required EF Core implementation of Identity stores
                .AddEntityFrameworkStores<IdentitySchoolLibraryDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddScoped<IUserManager, UserManagerService>();
        }
    }
}