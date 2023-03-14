using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class BookGenresService : IBookGenresService
    {
        IUnitOfWork _uow;

        public BookGenresService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CreateAsync(BookGenres entity)
        {
            var id = await _uow.BookGenres.CreateAsync(entity);
            _uow.Commit();

            return id;
        }
        public async Task<BookGenres> GetAsync(Guid id)
        {
            return await _uow.BookGenres.GetByIdAsync(id);
        }
        public async Task<IEnumerable<BookGenres>> GetAllAsync()
        {
            return await _uow.BookGenres.GetAllAsync();
        }
        public async Task UpdateAsync(BookGenres entity)
        {
            await _uow.BookGenres.UpdateAsync(entity);
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.BookGenres.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
