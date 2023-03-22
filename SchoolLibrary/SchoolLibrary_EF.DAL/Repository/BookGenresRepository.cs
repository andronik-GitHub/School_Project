using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class BookGenresRepository : GenericIntermediateRepository<BookGenres>, IBookGenresRepository
    {
        public BookGenresRepository(SchoolLibraryContext dbContext)
            : base(dbContext)
        {
        }


        public override async Task<IEnumerable<BookGenres>> GetAllAsync()
        {
            entities.ToList().ForEach(bookGenres =>
                dbContext.BookGenres
                    .Include(bg => bg.Book)
                    .Include(bg => bg.Genre)
                    .FirstOrDefault(bg => bg.BookId == bookGenres.BookId && bg.GenreId == bookGenres.GenreId)
                );

            return await entities.ToListAsync();
        }
        public override async Task<BookGenres?> GetByIdAsync(Guid firstId, Guid secondId)
        {
            return await entities
                .Include(bg => bg.Book)
                .Include(bg => bg.Genre)
                .FirstOrDefaultAsync(bg => bg.BookId == firstId && bg.GenreId == secondId);
        }
    }
}
