using AutoMapper;
using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Dapper.BLL.DTOs.UserDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(InsertDTO_User entity)
        {
            User newEntity = _mapper.Map<User>(entity); // Mapping without AutoMapper

            var id = await _uow.Users.CreateAsync(newEntity);
            _uow.Commit();

            return id;
        }
        public async Task<GetDTO_User?> GetAsync(Guid id)
        {
            var entity = await _uow.Users.GetByIdAsync(id);

            GetDTO_User? dto = _mapper.Map<GetDTO_User?>(entity); // Mapping without AutoMapper
            
            return dto;
        }
        public async Task<IEnumerable<GetDTO_User>> GetAllAsync()
        {
            // Use AutoMapper to project one collection onto another
            var list = _mapper.Map<IEnumerable<User>, IEnumerable<GetDTO_User>>(await _uow.Users.GetAllAsync());
            return list;
        }
        public async Task UpdateAsync(UpdateDTO_User entity)
        {
            User upEntity = _mapper.Map<User>(entity); // Mapping without AutoMapper

            await _uow.Users.UpdateAsync(upEntity);
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
