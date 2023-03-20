using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.BLL.Services;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Bogus;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Repositories;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

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

#region AddMainServices
{
    // Logging
    builder.Services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
    builder.Services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));

    // DbContext
    builder.Services.AddDbContext<SchoolLibraryContext>(options =>
    {
        options.UseSqlServer(
                builder.Configuration.GetConnectionString("sqlConnection"),
                options => options.MigrationsAssembly("SchoolLibrary_EF.API")
            );
    });

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // AutoMapper
    builder.Services.RegisterMapsterConfiguration(); // Mapster


    // BLL
    {
        builder.Services.AddScoped<IBookService, BookService>();
        //builder.Services.AddScoped<IBookDetailsService, BookDetailsService>();
        builder.Services.AddScoped<IAuthorService, AuthorService>();
        builder.Services.AddScoped<IPublisherService, PublisherService>();
        builder.Services.AddScoped<IUserService, UserService>();
        //builder.Services.AddScoped<ILoanService, LoanService>();
        //builder.Services.AddScoped<IReviewRService, ReviewService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
        //builder.Services.AddScoped<IBookGenresService, BookGenresService>();
        //builder.Services.AddScoped<IBookAuthorsService, BookAuthorsService>();
    }

    // DAL
    {
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
}
#endregion


DataGenerator.InitBogusData();
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


app.MapControllers();


app.Run();
