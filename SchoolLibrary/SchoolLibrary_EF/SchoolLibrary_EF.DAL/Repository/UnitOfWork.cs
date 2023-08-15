using Microsoft.AspNetCore.Identity;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public SchoolLibraryContext _dbContext { get; set; }
        public IBookRepository Books { get; }
        public IBookDetailsRepository BookDetails { get; }
        public IAuthorRepository Authors { get; }
        public IPublisherRepository Publishers { get; }
        public IUserRepository Users { get; }
        public ILoanRepository Loans { get; }
        public IReviewRepository Reviews { get; }
        public IGenreRepository Genres { get; }
        public IBookGenresRepository BookGenres { get; }
        public IBookAuthorsRepository BookAuthors { get; }
        
        
        public UserManager<User> _userManager { get; set; }
        public RoleManager<IdentityRole<Guid>> _roleManager { get; set; }

        public UnitOfWork(
            SchoolLibraryContext dbContext,

            IBookRepository books,
            IBookDetailsRepository bookDetails,
            IAuthorRepository authors,
            IPublisherRepository publishers,
            IUserRepository users,
            ILoanRepository loans,
            IReviewRepository reviews,
            IGenreRepository genres,
            IBookGenresRepository bookGenres,
            IBookAuthorsRepository bookAuthors,
            
            UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager
            )
        {
            _dbContext = dbContext;

            Books = books;
            BookDetails = bookDetails;
            Authors = authors;
            Publishers = publishers;
            Users = users;
            Loans = loans;
            Reviews = reviews;
            Genres = genres;
            BookGenres = bookGenres;
            BookAuthors = bookAuthors;

            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
