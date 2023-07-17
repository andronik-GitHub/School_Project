using System.Dynamic;
using AutoMapper;
using SchoolLibrary_EF.BLL.DTOs.GenreDTOs;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.BLL.Services
{
    public class GenreService : IGenreService
    {
        private IUnitOfWork _uow { get; set; }
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<Guid> CreateAsync(InsertDTO_Genre entity)
        {
            Genre genre = _mapper.Map<Genre>(entity); // Mapping with AutoMapper

            var id = await _uow.Genres.CreateAsync(genre);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<GetDTO_Genre>> GetAllAsync(BaseParameters parameters)
        {
            // Use AutoMapper to project one collection onto another
            return _mapper.Map<IEnumerable<Genre>, IEnumerable<GetDTO_Genre>>
                (await _uow.Genres.GetAllAsync(parameters));
        }
        public async Task<GetDTO_Genre?> GetAsync(Guid id)
        {
            Genre? genre = await _uow.Genres.GetByIdAsync(id);

            GetDTO_Genre? genreDTO = _mapper.Map<GetDTO_Genre?>(genre); // Mapping with AutoMapper

            return genreDTO;
        }
        public async Task UpdateAsync(UpdateDTO_Genre entity)
        {
            Genre genre = _mapper.Map<Genre>(entity); // Mapping with AutoMapper

            await _uow.Genres.UpdateAsync(genre);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Genres.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters)
        {
            return await _uow.Genres.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters parameters)
        {
            return await _uow.Genres.GetById_DataShaping_Async(id, parameters);
        }
    }
}
