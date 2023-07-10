using AutoMapper;
using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Dapper.BLL.DTOs.AuthorDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(InsertDTO_Author entity)
        {
            Author newEntity = _mapper.Map<Author>(entity); // Mapping without AutoMapper
            
            var id = await _uow.Authors.CreateAsync(newEntity);
            _uow.Commit();

            return id;
        }
        public async Task<GetDTO_Author?> GetAsync(Guid id)
        {
            var entity =  await _uow.Authors.GetByIdAsync(id);

            GetDTO_Author? dto = _mapper.Map<GetDTO_Author?>(entity); // Mapping without AutoMapper
            
            return dto;
        }
        public async Task<IEnumerable<GetDTO_Author>> GetAllAsync()
        {
            // Use AutoMapper to project one collection onto another
            var list = _mapper.Map<IEnumerable<Author>, IEnumerable<GetDTO_Author>>(await _uow.Authors.GetAllAsync());
            return list;
        }
        public async Task UpdateAsync(UpdateDTO_Author entity)
        {
            Author upEntity = _mapper.Map<Author>(entity); // Mapping without AutoMapper
            
            await _uow.Authors.UpdateAsync(upEntity);
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Authors.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
