using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public SchoolLibraryContext dbContext { get; set; }
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

        public ISortHelper<Book> _bookSortHelper { get; set; }
        public ISortHelper<BookDetails> _bookDetailsSortHelper { get; set; }

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

            ISortHelper<Book> bookSortHelper,
            ISortHelper<BookDetails> bookDetailsSortHelper
            )
        {
            this.dbContext = dbContext;

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

            _bookSortHelper = bookSortHelper;
            _bookDetailsSortHelper = bookDetailsSortHelper;
        }


        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
