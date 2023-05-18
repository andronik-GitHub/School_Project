using System.Text;
using Application.Common.Interfaces;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Models.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuration from appsettings.json
            services.Configure<JWT>(configuration.GetSection("JWT"));
            
            services
                // Register ASP.NET Core Identity with method AddIdentity<TUser, TRole>
                .AddIdentity<UserIdentity, IdentityRole<Guid>>()
                // To register the required EF Core implementation of Identity stores
                .AddEntityFrameworkStores<IdentitySchoolLibraryDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IUserManager, UserManagerService>(); // Register UserManager
            
            
            // Register IdentityDbContext
            services.AddDbContext<IdentitySchoolLibraryDbContext>(options =>
            {
                options
                    .UseSqlServer(
                        configuration.GetConnectionString("sqlConnection"),
                        b => b.MigrationsAssembly(typeof(IdentitySchoolLibraryDbContext).Assembly.GetName().Name)
                    )
                    .EnableSensitiveDataLogging();
            });
            
            
            // Register Authentication - JWT
            services
                // Adding Authentication
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                // Adding Scheme JwtBearer
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false;
                    
                    // Parameters for validating JWT tokens
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true, // whether lifetime will be validated
                        // Фllow time deviation when checking JWT token expiration
                        ClockSkew = TimeSpan.Zero,
                        
                        ValidIssuer = configuration["JWT:Issuer"] ?? "SecureCA_Api",
                        ValidAudience = configuration["JWT:Audience"] ?? "SecureCA_ApiUser",
                        IssuerSigningKey = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(configuration["JWT:Key"] ?? "C1CA1B7DC4C4175B6288DE4F55CA4")),
                    };
                });
        }
    }
}