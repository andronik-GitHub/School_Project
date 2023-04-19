using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class BookAuthorsRepository : GenericIntermediateRepository<BookAuthors>, IBookAuthorsRepository
    {
        private readonly ISortHelper<BookAuthors> _sortHelper;

        public BookAuthorsRepository(SchoolLibraryContext dbContext, ISortHelper<BookAuthors> sortHelper)
            : base(dbContext)
        {
            _sortHelper = sortHelper;
        }


        public override async Task<IEnumerable<BookAuthors>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync();


            var collection = entities.AsNoTracking();

            if (parameters is BookAuthorsParameters param)
            {
                var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

                return await newCollection
                    //.OrderBy(entity => entity.BookId) after sorting, it makes no sense to sort by id
                    //.ThenBy(entity => entity.AuthorId)
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .Include(entity => entity.Book)
                    .Include(entity => entity.Author)
                    .ToListAsync();
            }

            return await collection
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
