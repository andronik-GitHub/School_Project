using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Pagging.Entities;
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


        public override async Task<IEnumerable<BookGenres>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync();

            return await entities
                .OrderBy(entity => entity.BookId)
                .ThenBy(entity => entity.GenreId)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Include(entity => entity.Book)
                .Include(entity => entity.Genre)
                .ToListAsync();
        }
        public override async Task<BookGenres?> GetByIdAsync(Guid firstId, Guid secondId)
        {
            return await entities
                .Include(entity => entity.Book)
                .Include(entity => entity.Genre)
                .FirstOrDefaultAsync(entity => entity.BookId == firstId && entity.GenreId == secondId);
        }
    }
}
