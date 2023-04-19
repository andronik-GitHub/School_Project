using Microsoft.AspNetCore.Identity;
using SchoolLibrary_EF.DAL.Entities;

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
        
        
        UserManager<User> _userManager { get; set; }
        RoleManager<IdentityRole<Guid>> _roleManager { get; set; }


        Task SaveChangesAsync();
    }
}
