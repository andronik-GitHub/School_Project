using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.BLL.Services;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Bogus;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Repositories;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;
using SchoolLibrary_EF.DAL.Repository.Contracts;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

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
        builder.Services.AddScoped<IAuthorService, AuthorService>();
        builder.Services.AddScoped<IPublisherService, PublisherService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
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
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}


app.MapControllers();


app.Run();
