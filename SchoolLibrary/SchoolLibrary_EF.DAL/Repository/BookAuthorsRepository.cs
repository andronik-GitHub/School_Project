using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
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


        public override async Task<IEnumerable<BookAuthors>> GetAllAsync()
        {
            entities.ToList().ForEach(bookAuthors =>
                dbContext.BookAuthors
                    .Include(bg => bg.Book)
                    .Include(bg => bg.Author)
                    .FirstOrDefault(bg => bg.BookId == bookAuthors.BookId && bg.AuthorId == bookAuthors.AuthorId)
                );

            return await entities.ToListAsync();
        }
        public override async Task<BookAuthors?> GetByIdAsync(Guid firstId, Guid secondId)
        {
            return await entities
                .Include(bg => bg.Book)
                .Include(bg => bg.Author)
                .FirstOrDefaultAsync(bg => bg.BookId == firstId && bg.AuthorId == secondId);
        }
    }
}
