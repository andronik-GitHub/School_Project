using Mapster;
using SchoolLibrary_EF.DAL.Entities;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using SchoolLibrary_EF.BLL.DTOs.BookAuthorDTOs;
using SchoolLibrary_EF.BLL.DTOs.BookDetailsDTOs;
using SchoolLibrary_EF.BLL.DTOs.BookDTOs;
using SchoolLibrary_EF.BLL.DTOs.BookGenreDTOs;
using SchoolLibrary_EF.BLL.DTOs.LoanDTOs;
using SchoolLibrary_EF.BLL.DTOs.PublisherDTOs;
using SchoolLibrary_EF.BLL.DTOs.ReviewDTOs;
using SchoolLibrary_EF.DAL.Entities.Identity;

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
            RegisterAddRoleModel();


            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }

        private static void RegisterPublisherConfig()
        {
            TypeAdapterConfig<Publisher, GetDTO_Publisher>
                .NewConfig()
                .Map(dest => dest.PublisherId, src => src.PublisherId)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Country, src => src.Location.Split(new [] { ',' })[0] ?? "CountryEmpty, ")
                .Map(dest => dest.City, src => src.Location.Split(new [] { ',' })[1] ?? "CityEmpty, ")
                .Map(dest => dest.Street, src => src.Location.Split(new [] { ',' })[2] ?? "StreetEmpty")
                .TwoWays();
            
            TypeAdapterConfig<Publisher, InsertDTO_Publisher>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Location, src => src.Location)
                .TwoWays();
            
            TypeAdapterConfig<Publisher, UpdateDTO_Publisher>
                .NewConfig()
                .Map(dest => dest.PublisherId, src => src.PublisherId)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Location, src => src.Location)
                .TwoWays();
        }
        private static void RegisterBookConfig()
        {
            TypeAdapterConfig<Book, GetDTO_Book>
                .NewConfig()
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.ISBN, src => src.ISBN)
                .Map(dest => dest.PublisherName, src => src.Publisher.Name)
                .Map(dest => dest.PublisherLocation, src => src.Publisher.Location)
                .TwoWays();

            TypeAdapterConfig<Book, InsertDTO_Book>
                .NewConfig()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.PublishingYear, src => src.PublishingYear)
                .Map(dest => dest.ISBN, src => src.ISBN)
                .Map(dest => dest.PublisherId, src => src.PublisherId)
                .TwoWays();
            
            TypeAdapterConfig<Book, UpdateDTO_Book>
                .NewConfig()
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.PublishingYear, src => src.PublishingYear)
                .Map(dest => dest.ISBN, src => src.ISBN)
                .Map(dest => dest.PublisherId, src => src.PublisherId)
                .TwoWays();
        }
        private static void RegisterBookDetailsConfig()
        {
            TypeAdapterConfig<BookDetails, GetDTO_BookDetails>
                .NewConfig()
                .Map(dest => dest.BookDetailId, src => src.BookDetailId)
                .Map(dest => dest.Pages, src => src.Pages)
                .Map(dest => dest.Language, src => src.Language)
                .Map(dest => dest.BookTitle, src => src.Book.Title)
                .Map(dest => dest.ISBN, src => src.Book.ISBN)
                .TwoWays();
            
            TypeAdapterConfig<BookDetails, InsertDTO_BookDetails>
                .NewConfig()
                .Map(dest => dest.Pages, src => src.Pages)
                .Map(dest => dest.Language, src => src.Language)
                .Map(dest => dest.Format, src => src.Format)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
            
            TypeAdapterConfig<BookDetails, UpdateDTO_BookDetails>
                .NewConfig()
                .Map(dest => dest.BookDetailId, src => src.BookDetailId)
                .Map(dest => dest.Pages, src => src.Pages)
                .Map(dest => dest.Language, src => src.Language)
                .Map(dest => dest.Format, src => src.Format)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
        }
        private static void RegisterLoanConfig()
        {
            TypeAdapterConfig<Loan, GetDTO_Loan>
                .NewConfig()
                .Map(dest => dest.LoanId, src => src.LoanId)
                .Map(dest => dest.LoanDate, src => src.LoanDate)
                .Map(dest => dest.ReturnDate, src => src.ReturnDate)
                .Map(dest => dest.UserFullName, src => $"{src.User.FirstName} {src.User.LastName}")
                .Map(dest => dest.BookTitle, src => src.Book.Title)
                .Map(dest => dest.BookISBN, src => src.Book.ISBN)
                .TwoWays();
            
            TypeAdapterConfig<Loan, InsertDTO_Loan>
                .NewConfig()
                .Map(dest => dest.LoanId, src => src.LoanId)
                .Map(dest => dest.LoanDate, src => src.LoanDate)
                .Map(dest => dest.ReturnDate, src => src.ReturnDate)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
            
            TypeAdapterConfig<Loan, UpdateDTO_Loan>
                .NewConfig()
                .Map(dest => dest.LoanId, src => src.LoanId)
                .Map(dest => dest.LoanDate, src => src.LoanDate)
                .Map(dest => dest.ReturnDate, src => src.ReturnDate)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
        }
        private static void RegisterReviewConfig()
        {
            TypeAdapterConfig<Review, GetDTO_Review>
                .NewConfig()
                .Map(dest => dest.ReviewId, src => src.ReviewId)
                .Map(dest => dest.Rating, src => src.Rating)
                .Map(dest => dest.ReviewText, src => src.ReviewText)
                .Map(dest => dest.UserFullName, src => $"{src.User.FirstName} {src.User.LastName}")
                .Map(dest => dest.BookTitle, src => src.Book.Title)
                .TwoWays();
            
            TypeAdapterConfig<Review, InsertDTO_Review>
                .NewConfig()
                .Map(dest => dest.Rating, src => src.Rating)
                .Map(dest => dest.ReviewText, src => src.ReviewText)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
            
            TypeAdapterConfig<Review, UpdateDTO_Review>
                .NewConfig()
                .Map(dest => dest.Rating, src => src.Rating)
                .Map(dest => dest.ReviewText, src => src.ReviewText)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
        }
        private static void RegisterBookGenresConfig()
        {
            TypeAdapterConfig<BookGenres, GetDTO_BookGenres>
                .NewConfig()
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.GenreId, src => src.GenreId)
                .Map(dest => dest.BookTitle, src => src.Book.Title)
                .Map(dest => dest.GenreName, src => src.Genre.Name)
                .TwoWays();
            
            TypeAdapterConfig<BookGenres, InsertDTO_BookGenres>
                .NewConfig()
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.GenreId, src => src.GenreId)
                .TwoWays();
            
            TypeAdapterConfig<BookGenres, UpdateDTO_BookGenres>
                .NewConfig()
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.GenreId, src => src.GenreId)
                .TwoWays();
        }
        private static void RegisterBookAuthorsConfig()
        {
            TypeAdapterConfig<BookAuthors, GetDTO_BookAuthors>
                .NewConfig()
                .Map(dest => dest.AuthorId, src => src.AuthorId)
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.BookTitle, src => src.Book.Title)
                .Map(dest => dest.AuthorFullName, src => $"{src.Author.FirstName} {src.Author.LastName}")
                .TwoWays();
            
            TypeAdapterConfig<BookAuthors, InsertDTO_BookAuthors>
                .NewConfig()
                .Map(dest => dest.AuthorId, src => src.AuthorId)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
            
            TypeAdapterConfig<BookAuthors, UpdateDTO_BookAuthors>
                .NewConfig()
                .Map(dest => dest.AuthorId, src => src.AuthorId)
                .Map(dest => dest.BookId, src => src.BookId)
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
        private static void RegisterAddRoleModel()
        {
            TypeAdapterConfig<RegisterModel, AddRoleModel>
                .NewConfig()
                .TwoWays()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password);
        }
    }
}
