using SchoolLibrary_Dapper.DAL.Repositories;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
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
