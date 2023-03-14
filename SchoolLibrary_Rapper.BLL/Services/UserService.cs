using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CreateAsync(User entity)
        {
            var id = await _uow.Users.CreateAsync(entity);
            _uow.Commit();

            return id;
        }
        public async Task<User> GetAsync(Guid id)
        {
            return await _uow.Users.GetByIdAsync(id);
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _uow.Users.GetAllAsync();
        }
        public async Task UpdateAsync(User entity)
        {
            await _uow.Users.UpdateAsync(entity);
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Users.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
