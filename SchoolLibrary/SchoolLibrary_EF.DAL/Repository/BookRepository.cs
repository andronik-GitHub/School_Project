using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Pagging.Entities;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ISortHelper<Book> _sortHelper;

        public BookRepository(SchoolLibraryContext dbContext, ISortHelper<Book> sortHelper)
            : base(dbContext)
        {
            _sortHelper = sortHelper;
        }


        public override async Task<Guid> CreateAsync(Book book)
        {
            await entities.AddAsync(book);

            return book.BookId;
        }
        public override async Task<IEnumerable<Book>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync();


            if (parameters is BookParameters param)
            {
                var collection = entities.AsNoTracking();

                var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

                return await newCollection
                    //.OrderBy(entity => entity.BookId) after sorting, it makes no sense to sort by id
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .Include(entity => entity.Publisher)
                    .ToListAsync();
            }

            return await entities
                .AsNoTracking()
                .OrderBy(entity => entity.BookId)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Include(entity => entity.Publisher)
                .ToListAsync();
        }
        public override async Task<Book?> GetByIdAsync(Guid id)
        {
            return await entities.Include(b => b.Publisher).FirstOrDefaultAsync(b => b.BookId == id);
        }
    }
}
