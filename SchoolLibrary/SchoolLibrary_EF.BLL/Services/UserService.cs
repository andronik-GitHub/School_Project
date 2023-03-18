using AutoMapper;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _uow { get; set; }
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<Guid> CreateAsync(UserDTO entity)
        {
            // We create a User object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            User user = _mapper.Map<User>(entity);

            var id = await _uow.Users.CreateAsync(user);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            // Use AutoMapper to project one collection onto another
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await _uow.Users.GetAllAsync());
        }
        public async Task<UserDTO?> GetAsync(Guid id)
        {
            // Get entity from db
            User? user = await _uow.Users.GetByIdAsync(id);

            // We create a UserDTO object and copy the values ​​of the properties
            // of the user object into its properties (we perform mapping)
            UserDTO? userDTO = _mapper.Map<UserDTO?>(user);

            return  userDTO;
        }
        public async Task UpdateAsync(UserDTO entity)
        {
            // We create a User object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            User user = _mapper.Map<User>(entity);

            await _uow.Users.UpdateAsync(user);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Users.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
