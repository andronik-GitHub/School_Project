using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.BLL.Services;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Bogus;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Helpers;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Repository;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using SchoolLibrary_EF.API.Validation;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.DAL.Entities.Identity;
using SchoolLibrary_EF.DAL.Helpers.Contracts;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


#region AddMainServices
{
    #region API
    {
        // Logging
        builder.Services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
        builder.Services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));

        // DbContext
        builder.Services.AddDbContext<SchoolLibraryContext>(options =>
        {
            options.UseSqlServer(
                    builder.Configuration.GetConnectionString("sqlConnection"),
                    op => op.MigrationsAssembly("SchoolLibrary_EF.API")
                )
                .EnableSensitiveDataLogging();
        });

        #region IDENTITY
        {
            // Reads data from our previously created JWT Section of appsettings.json
            builder.Services.Configure<JWT>(configuration.GetSection("JWT"));
            
            // Register ASP.NET Core Identity with method AddIdentity<TUser, TRole>
            builder.Services.AddIdentity<User, IdentityRole<Guid>>()
                // To register the required EF Core implementation of Identity stores
                .AddEntityFrameworkStores<SchoolLibraryContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                // Adding JwtBearer
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JWT:Key"] ?? "C1CF4B7DC4C417"))
                    };
                });
        }
        #endregion

        // Mapping
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // AutoMapper
        builder.Services.RegisterMapsterConfiguration(); // Mapster

        // HATEOAS
        builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        builder.Services.AddScoped<IUrlHelper>(x =>
        {
            var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
            var factory = x.GetRequiredService<IUrlHelperFactory>();
            return factory.GetUrlHelper(actionContext!);
        });

        // Validation
        builder.Services.AddTransient<IValidator<UserDTO>, UserDTO_Validator>();
        builder.Services.AddTransient<IValidator<RegisterModel>, RegisterModel_Validator>();
        builder.Services.AddTransient<IValidator<LoginModel>, LoginModel_Validator>();
        builder.Services.AddTransient<IValidator<AuthenticationModel>, AuthenticationModel_Validator>();
    }
    #endregion

    #region BLL
    {
        // Services
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IBookDetailsService, BookDetailsService>();
        builder.Services.AddScoped<IAuthorService, AuthorService>();
        builder.Services.AddScoped<IPublisherService, PublisherService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ILoanService, LoanService>();
        builder.Services.AddScoped<IReviewService, ReviewService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
        builder.Services.AddScoped<IBookGenresService, BookGenresService>();
        builder.Services.AddScoped<IBookAuthorsService, BookAuthorsService>();
    }
    #endregion

    #region DAL
    {
        // For implementation sorting
        builder.Services.AddScoped<ISortHelper<Book>, SortHelper<Book>>();
        builder.Services.AddScoped<ISortHelper<BookDetails>, SortHelper<BookDetails>>();
        builder.Services.AddScoped<ISortHelper<Author>, SortHelper<Author>>();
        builder.Services.AddScoped<ISortHelper<Publisher>, SortHelper<Publisher>>();
        builder.Services.AddScoped<ISortHelper<User>, SortHelper<User>>();
        builder.Services.AddScoped<ISortHelper<Loan>, SortHelper<Loan>>();
        builder.Services.AddScoped<ISortHelper<Review>, SortHelper<Review>>();
        builder.Services.AddScoped<ISortHelper<Genre>, SortHelper<Genre>>();
        builder.Services.AddScoped<ISortHelper<BookGenres>, SortHelper<BookGenres>>();
        builder.Services.AddScoped<ISortHelper<BookAuthors>, SortHelper<BookAuthors>>();

        // For implementation data shaping
        builder.Services.AddScoped<IDataShaper<Book>, DataShaper<Book>>();
        builder.Services.AddScoped<IDataShaper<BookDetails>, DataShaper<BookDetails>>();
        builder.Services.AddScoped<IDataShaper<Author>, DataShaper<Author>>();
        builder.Services.AddScoped<IDataShaper<Publisher>, DataShaper<Publisher>>();
        builder.Services.AddScoped<IDataShaper<User>, DataShaper<User>>();
        builder.Services.AddScoped<IDataShaper<Loan>, DataShaper<Loan>>();
        builder.Services.AddScoped<IDataShaper<Review>, DataShaper<Review>>();
        builder.Services.AddScoped<IDataShaper<Genre>, DataShaper<Genre>>();

        // Pattern Repository
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IBookDetailsRepository, BookDetailsRepository>();
        builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
        builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ILoanRepository, LoanRepository>();
        builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
        builder.Services.AddScoped<IGenreRepository, GenreRepository>();
        builder.Services.AddScoped<IBookGenresRepository, BookGenresRepository>();
        builder.Services.AddScoped<IBookAuthorsRepository, BookAuthorsRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    #endregion
}
#endregion


builder.Services.AddControllers()
    // Resolving Json Serialization Problems(Data Shaping)
    .AddNewtonsoftJson() // NuGet package - Microsoft.AspNetCore.Mvc.NewtonsoftJson
    .AddFluentValidation(); // Registration FluentValidation(For registration of validators)

// Register a Swagger generator by defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "School Library",
        Version = "v1",
        Description = "API for performing operations with \"School Library\"",
    });

    // Customizing the comment path for Swagger JSON and UI
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);
});


// DataGenerator.InitBogusData(); // seeding db
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
