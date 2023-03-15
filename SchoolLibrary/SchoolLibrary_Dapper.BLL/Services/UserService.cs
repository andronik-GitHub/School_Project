using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Dapper.BLL.DTO;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CreateAsync(UserDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow.Users.CreateAsync(new User
            {
                UserId = entity.UserId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Password = entity.Password,
                Address = entity.Address,
                Phone = entity.Phone,
            });
            _uow.Commit();

            return id;
        }
        public async Task<UserDTO> GetAsync(Guid id)
        {
            var entity = await _uow.Users.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new UserDTO
            {
                UserId = entity.UserId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Password = entity.Password,
                Address = entity.Address,
                Phone = entity.Phone,
            };
        }
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var list = await _uow.Users.GetAllAsync();
            var result = new List<UserDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new UserDTO
            {
                UserId = entity.UserId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Password = entity.Password,
                Address = entity.Address,
                Phone = entity.Phone,
            }));

            return result;
        }
        public async Task UpdateAsync(UserDTO entity)
        {
            // Mapping without AutoMapper
            await _uow.Users.UpdateAsync(new User
            {
                UserId = entity.UserId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Password = entity.Password,
                Address = entity.Address,
                Phone = entity.Phone,
            });
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
