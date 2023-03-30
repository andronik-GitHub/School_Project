using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Paging.Entities;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        private readonly ISortHelper<Genre> _sortHelper;
        public GenreRepository(SchoolLibraryContext dbContext, ISortHelper<Genre> sortHelper)
            : base(dbContext)
        {
            _sortHelper = sortHelper;
        }


        public override async Task<Guid> CreateAsync(Genre genre)
        {
            await entities.AddAsync(genre);

            return genre.GenreId;
        }

        public override async Task<IEnumerable<Genre>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync(parameters);


            var collection = entities.AsNoTracking();

            if (parameters is GenreParameters param)
            {
                var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

                return await newCollection
                    //.OrderBy(entity => entity.GenreId) after sorting, it makes no sense to sort by id
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();
            }

            return await collection
                .OrderBy(entity => entity.GenreId)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
        }
    }
}
