using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Paging.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Helper.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ISortHelper<User> _sortHelper;
        public UserRepository(SchoolLibraryContext dbContext, ISortHelper<User> sortHelper)
            : base(dbContext)
        {
            _sortHelper = sortHelper;
        }


        public override async Task<Guid> CreateAsync(User user)
        {
            await entities.AddAsync(user);

            return user.UserId;
        }
        public override async Task<IEnumerable<User>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync();


            var collection = entities.AsNoTracking(); // filtering

            if (parameters is UserParameters param) // filtering/searching
            {
                SearchByUserName(ref collection, param.UserName); // searching(after filtering)

                var newCollection = _sortHelper.ApplySort(collection, param.OrderBy); // sorting

                return await newCollection
                    //.OrderBy(a => a.UserId) after sorting, it makes no sense to sort by id
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();
            }

            return await collection
                .OrderBy(a => a.UserId)
                .Skip((parameters!.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
        }

        private static void SearchByUserName(ref IQueryable<User> entities, string? userName)
        {
            if (!entities.Any() || string.IsNullOrWhiteSpace(userName)) return;

            entities = entities
                .Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(userName.Trim().ToLower()));
        }
    }
}
