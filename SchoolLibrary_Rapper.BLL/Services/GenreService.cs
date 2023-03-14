using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
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

        public async Task<Guid> CreateAsync(Genre entity)
        {
            var id = await _uow.Genres.CreateAsync(entity);
            _uow.Commit();

            return id;
        }
        public async Task<Genre> GetAsync(Guid id)
        {
            return await _uow.Genres.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _uow.Genres.GetAllAsync();
        }
        public async Task UpdateAsync(Genre entity)
        {
            await _uow.Genres.UpdateAsync(entity);
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
