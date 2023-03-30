using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.DAL.Repository.Contracts
{
    public interface IUnitOfWork
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


        Task SaveChangesAsync();
    }
}
