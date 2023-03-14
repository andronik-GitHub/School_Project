using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class BookService : IBookService
    {
        IUnitOfWork _uow { get; set; }

        public BookService(IUnitOfWork unitOfWork)
        {
            this._uow = unitOfWork;
        }


        public async Task<Guid> CreateAsync(Book entity)
        {
            var id = await _uow.Books.CreateAsync(entity);
            _uow.Commit();

            return id;
        }
        public async Task<Book> GetAsync(Guid id)
        {
            return await _uow.Books.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _uow.Books.GetAllAsync();
        }
        public async Task UpdateAsync(Book entity)
        {
            await _uow.Books.UpdateAsync(entity);
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Books.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
