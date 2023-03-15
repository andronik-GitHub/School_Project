namespace SchoolLibrary_Dapper.DAL.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IBookDetailsRepository BookDetails { get; }
        IAuthorRepository Authors { get; }
        IPublisherRepository Publishers { get; }
        IUserRepository Users { get; }
        ILoanRepository Loans { get; }
        IReviewRepository Reviews { get; }
        IGenreRepository Genres { get; }
        IBookGenresRepository BookGenres { get; }
        IBookAuthorsRepository BookAuthors { get; }

        void Commit();
    }
}
