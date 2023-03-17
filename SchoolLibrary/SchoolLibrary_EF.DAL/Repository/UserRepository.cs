using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;
using SchoolLibrary_EF.DAL.Data;

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
    }
}
