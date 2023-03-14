using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class BookAuthorsService : IBookAuthorsService
    {
        IUnitOfWork _uow;

        public BookAuthorsService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CreateAsync(BookAuthors entity)
        {
            var id = await _uow.BookAuthors.CreateAsync(entity);
            _uow.Commit();

            return id;
        }
        public async Task<BookAuthors> GetAsync(Guid id)
        {
            return await _uow.BookAuthors.GetByIdAsync(id);
        }
        public async Task<IEnumerable<BookAuthors>> GetAllAsync()
        {
            return await _uow.BookAuthors.GetAllAsync();
        }
        public async Task UpdateAsync(BookAuthors entity)
        {
            await _uow.BookAuthors.UpdateAsync(entity);
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.BookAuthors.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
