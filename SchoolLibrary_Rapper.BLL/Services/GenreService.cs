using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.DTO;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class GenreService : IGenreService
    {
        IUnitOfWork _uow;

        public GenreService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CreateAsync(GenreDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow.Genres.CreateAsync(new Genre
            {
                GenreId = entity.GenreId,
                Name = entity.Name,
            });
            _uow.Commit();

            return id;
        }
        public async Task<GenreDTO> GetAsync(Guid id)
        {
            var entity = await _uow.Genres.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new GenreDTO
            {
                GenreId = entity.GenreId,
                Name = entity.Name,
            };
        }
        public async Task<IEnumerable<GenreDTO>> GetAllAsync()
        {
            var list = await _uow.Genres.GetAllAsync();
            var result = new List<GenreDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new GenreDTO
            {
                GenreId = entity.GenreId,
                Name = entity.Name,
            }));

            return result;
        }
        public async Task UpdateAsync(GenreDTO entity)
        {
            // Mapping without AutoMapper
            await _uow.Genres.UpdateAsync(new Genre
            {
                GenreId = entity.GenreId,
                Name = entity.Name,
            });
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
