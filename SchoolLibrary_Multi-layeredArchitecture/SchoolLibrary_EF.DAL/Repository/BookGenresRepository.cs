using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class BookGenresRepository : GenericIntermediateRepository<BookGenres>, IBookGenresRepository
    {
        private readonly ISortHelper<BookGenres> _sortHelper;
        public BookGenresRepository(SchoolLibraryContext dbContext, ISortHelper<BookGenres> sortHelper)
            : base(dbContext)
        {
            _sortHelper = sortHelper;
        }


        public override async Task<IEnumerable<BookGenres>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync();


            var collection = entities.AsNoTracking();

            if (parameters is BookGenresParameters param)
            {
                var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

                return await newCollection
                    //.OrderBy(entity => entity.BookId) after sorting, it makes no sense to sort by id
                    //.ThenBy(entity => entity.GenreId)
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .Include(entity => entity.Book)
                    .Include(entity => entity.Genre)
                    .ToListAsync();
            }

            return await collection
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
