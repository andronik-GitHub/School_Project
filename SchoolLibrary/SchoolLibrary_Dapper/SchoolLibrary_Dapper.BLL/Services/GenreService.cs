using AutoMapper;
using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Dapper.BLL.DTOs.GenreDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(InsertDTO_Genre entity)
        {
            Genre newEntity = _mapper.Map<Genre>(entity); // Mapping without AutoMapper
            
            var id = await _uow.Genres.CreateAsync(newEntity);
            _uow.Commit();

            return id;
        }
        public async Task<GetDTO_Genre?> GetAsync(Guid id)
        {
            var entity = await _uow.Genres.GetByIdAsync(id);

            GetDTO_Genre? dto = _mapper.Map<GetDTO_Genre?>(entity); // Mapping without AutoMapper
            
            return dto;
        }
        public async Task<IEnumerable<GetDTO_Genre>> GetAllAsync()
        {
            // Use AutoMapper to project one collection onto another
            var list = _mapper.Map<IEnumerable<Genre>, IEnumerable<GetDTO_Genre>>(await _uow.Genres.GetAllAsync());
            return list;
        }
        public async Task UpdateAsync(UpdateDTO_Genre entity)
        {
            Genre upEntity = _mapper.Map<Genre>(entity); // Mapping without AutoMapper

            await _uow.Genres.UpdateAsync(upEntity);
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Genres.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
