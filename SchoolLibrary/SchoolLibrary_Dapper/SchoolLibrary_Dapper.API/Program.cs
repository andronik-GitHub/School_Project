using SchoolLibrary_Dapper.DAL.Repositories;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Dapper.BLL.Services;
using SchoolLibrary_Dapper.BLL.Services.Consracts;
using System.Data;
using System.Data.SqlClient;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

#region AddServices
{
    builder.Services.AddScoped(provider =>
        new SqlConnection(builder.Configuration.GetConnectionString("sqlConnection")));
    builder.Services.AddScoped<IDbTransaction>(provider =>
    {
        var connection = provider.GetRequiredService<SqlConnection>();
        connection.Open();
        return connection.BeginTransaction();
    });

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

    // BLL
    {
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
}
#endregion


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
