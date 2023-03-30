using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Paging.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Helper.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class AuthorRepository : GenericRepository<Author> , IAuthorRepository
    {
        private readonly ISortHelper<Author> _sortHelper;

        public AuthorRepository(SchoolLibraryContext dbContext, ISortHelper<Author> sortHelper)
            : base(dbContext)
        {
            _sortHelper = sortHelper;
        }


        public override async Task<Guid> CreateAsync(Author author)
        {
            await entities.AddAsync(author);

            return author.AuthorId;
        }
        public override async Task<IEnumerable<Author>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync();


            var collection = entities.AsNoTracking();

            if (parameters is AuthorParameters param) // filtering
            {
                var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

                return await newCollection
                    //.OrderBy(a => a.AuthorId) after sorting, it makes no sense to sort by id
                    .Skip((parameters!.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();
            }

            return await collection
                .OrderBy(a => a.AuthorId)
                .Skip((parameters!.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
        }
    }
}
