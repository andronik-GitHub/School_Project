using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Pagging.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(SchoolLibraryContext dbContext)
            : base(dbContext)
        {
        }


        public override async Task<Guid> CreateAsync(User user)
        {
            await entities.AddAsync(user);

            return user.UserId;
        }
        public override async Task<IEnumerable<User>> GetAllAsync(BaseParameters? parameters = null)
        {
            if (parameters == null) return await base.GetAllAsync();

            if (parameters is UserParameters param) // filtering/searching
            {
                var collection = entities.AsNoTracking(); // filtering

                SearchByUserName(ref collection, param.UserName); // searching(after filtering)

                return await collection
                    .OrderBy(a => a.UserId)
                    .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                    .Take(parameters.PageSize)
                    .ToListAsync();
            }
            else
                return await entities
                    .AsNoTracking()
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
