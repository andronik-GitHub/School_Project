using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using System.Data;
using System.Data.Common;

namespace SchoolLibrary_Dapper.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
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

        readonly IDbTransaction dbTransaction;

        public UnitOfWork(
            IBookRepository Books,
            IBookDetailsRepository BookDetails,
            IAuthorRepository Authors,
            IPublisherRepository Publishers,
            IUserRepository Users,
            ILoanRepository Loans,
            IReviewRepository Reviews,
            IGenreRepository Genres,
            IBookGenresRepository BookGenres,
            IBookAuthorsRepository BookAuthors,

            IDbTransaction dbTransaction
            )
        {
            this.Books = Books;
            this.BookDetails = BookDetails;
            this.Authors = Authors;
            this.Publishers = Publishers;
            this.Users = Users;
            this.Loans = Loans;
            this.Reviews = Reviews;
            this.Genres = Genres;
            this.BookGenres = BookGenres;
            this.BookAuthors = BookAuthors;

            this.dbTransaction = dbTransaction;
        }


        public void Dispose()
        {
            // Закриття SQL під'єднання і видалення об'єктів
            dbTransaction.Connection?.Close();
            dbTransaction.Connection?.Dispose();
            dbTransaction.Dispose();
        }

        public void Commit()
        {
            try
            {
                dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
