using AutoMapper;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Pagging;
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


        public async Task<Guid> CreateAsync(GenreDTO entity)
        {
            // We create a Genre object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Genre genre = _mapper.Map<Genre>(entity);

            var id = await _uow.Genres.CreateAsync(genre);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<GenreDTO>> GetAllAsync(BaseParameters parameters)
        {
            // Use AutoMapper to project one collection onto another
            return _mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(await _uow.Genres.GetAllAsync(parameters));
        }
        public async Task<GenreDTO?> GetAsync(Guid id)
        {
            // Get entity from db
            Genre? genre = await _uow.Genres.GetByIdAsync(id);

            // We create a GenreDTO object and copy the values ​​of the properties
            // of the genre object into its properties (we perform mapping)
            GenreDTO? genreDTO = _mapper.Map<GenreDTO?>(genre);

            return genreDTO;
        }
        public async Task UpdateAsync(GenreDTO entity)
        {
            // We create a Genre object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Genre genre = _mapper.Map<Genre>(entity);

            await _uow.Genres.UpdateAsync(genre);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Genres.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
