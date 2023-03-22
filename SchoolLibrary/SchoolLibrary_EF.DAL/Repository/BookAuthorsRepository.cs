using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Pagging;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class BookAuthorsRepository : GenericIntermediateRepository<BookAuthors>, IBookAuthorsRepository
    {
        public BookAuthorsRepository(SchoolLibraryContext dbContext)
            : base(dbContext)
        {
        }


        public override async Task<IEnumerable<BookAuthors>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null)
                return await entities
                    .Include(entity => entity.Book)
                    .Include(entity => entity.Author)
                    .ToListAsync();

            return await entities
                .OrderBy(entity => entity.BookId)
                .ThenBy(entity => entity.AuthorId)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Include(entity => entity.Book)
                .Include(entity => entity.Author)
                .ToListAsync();
        }
        public override async Task<BookAuthors?> GetByIdAsync(Guid firstId, Guid secondId)
        {
            return await entities
                .Include(entity => entity.Book)
                .Include(entity => entity.Author)
                .FirstOrDefaultAsync(entity => entity.BookId == firstId && entity.AuthorId == secondId);
        }
    }
}
