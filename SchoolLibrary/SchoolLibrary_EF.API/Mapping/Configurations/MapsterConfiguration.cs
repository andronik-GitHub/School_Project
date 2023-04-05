using Mapster;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.DAL.Entities;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using SchoolLibrary_EF.BLL.DTO.Identity;

namespace SchoolLibrary_EF.API.Mapping.Configurations
{
    public static class MapsterConfiguration
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            RegisterBookConfig();
            RegisterBookDetailsConfig();
            //RegisterAuthorConfig(); in AutoMapper
            RegisterPublisherConfig(); // troubles in mapping
            //RegisterUserConfig(); in AutoMapper
            RegisterLoanConfig();
            RegisterReviewConfig();
            //RegisterGenreConfig(); in AutoMapper
            RegisterBookGenresConfig();
            RegisterBookAuthorsConfig();

            RegisterRegisterModelConfig();


            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }

        private static void RegisterPublisherConfig()
        {
            /*
             * При <PublisherDTO, Publisher>
             Якщо з БД витягувати об'єкти то получається:
                {
                    "publisherId": "117e5850-cfb4-4a2e-a21f-033035fe68fb",
                    "name": "Quigley, Spinka and Sawayn",
                    "country": null,
                    "city": null,
                    "street": null
                }
            а якщо робити вставку то ніяких помилок не виходить
            * При <Publisher, PublisherDTO>
            Якщо з БД витягувати об'єкти то ніяких помилок,
            а якщо робити вставку то вилітає Exception про вставку значень null в бд
            Пробував додати [Required] до всіх полів але це нічого не дало
             */
            TypeAdapterConfig<PublisherDTO, Publisher>
                .NewConfig()
                .Map(dest => dest.Location, src => $"{src.Street}, {src.City}, {src.Country}")
                // <Publisher, PublisherDTO>
                //.Map(dest => dest.Street, src => src.Location.Split(',', StringSplitOptions.None)[0].Trim())
                //.Map(dest => dest.City, src => src.Location.Split(',', StringSplitOptions.None)[1].Trim())
                //.Map(dest => dest.Country, src => src.Location.Split(',', StringSplitOptions.None)[2].Trim())
                .TwoWays();
        }
        private static void RegisterBookConfig()
        {
            TypeAdapterConfig<Book, BookDTO>
                .NewConfig()
                .Map(dest => dest.PublisherName, src => src.Publisher.Name)
                .Map(dest => dest.PublisherLocation, src => src.Publisher.Location)
                .TwoWays();
        }
        private static void RegisterBookDetailsConfig()
        {
            TypeAdapterConfig<BookDetails, BookDetailsDTO>
                .NewConfig()
                .Map(dest => dest.BookTitle, src => src.Book.Title)
                .Map(dest => dest.BookPublishingYear, src => src.Book.PublishingYear)
                .TwoWays();
        }
        private static void RegisterLoanConfig()
        {
            TypeAdapterConfig<Loan, LoanDTO>
                .NewConfig()
                .Map(dest => dest.UserFullName, src => $"{src.User.FirstName} {src.User.LastName}")
                .Map(dest => dest.BookTitle, src => src.Book.Title)
                .Map(dest => dest.BookISBN, src => src.Book.ISBN)
                .TwoWays();
        }
        private static void RegisterReviewConfig()
        {
            TypeAdapterConfig<Review, ReviewDTO>
                .NewConfig()
                .Map(dest => dest.UserFullName, src => $"{src.User.FirstName} {src.User.LastName}")
                .Map(dest => dest.BookTitle, src => src.Book.Title)
                .TwoWays();
        }
        private static void RegisterBookGenresConfig()
        {
            TypeAdapterConfig<BookGenres, BookGenresDTO>
                .NewConfig()
                .Map(dest => dest.BookTitle, src => src.Book.Title)
                .Map(dest => dest.GenreName, src => src.Genre.Name)
                .TwoWays();
        }
        private static void RegisterBookAuthorsConfig()
        {
            TypeAdapterConfig<BookAuthors, BookAuthorsDTO>
                .NewConfig()
                .Map(dest => dest.BookTitle, src => src.Book.Title)
                .Map(dest => dest.AuthorFullName, src => $"{src.Author.FirstName} {src.Author.LastName}")
                .TwoWays();
        }

        private static void RegisterRegisterModelConfig()
        {
            TypeAdapterConfig<RegisterModel, User>
                .NewConfig()
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.UserName, src => src.UserName)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PasswordHash, src => 
                    new PasswordHasher<User>(null).HashPassword(null!, src.Password!));
        }
    }
}
