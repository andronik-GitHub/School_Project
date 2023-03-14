using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class BookDetailsService : IBookDetailsService
    {
        IUnitOfWork _uow;

        public BookDetailsService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<Guid> CreateAsync(BookDetails entity)
        {
            var id = await _uow.BookDetails.CreateAsync(entity);
            _uow.Commit();

            return id;
        }
        public async Task<BookDetails> GetAsync(Guid id)
        {
            return await _uow.BookDetails.GetByIdAsync(id);
        }
        public async Task<IEnumerable<BookDetails>> GetAllAsync()
        {
            return await _uow.BookDetails.GetAllAsync();
        }
        public async Task UpdateAsync(BookDetails entity)
        {
            await _uow.BookDetails.UpdateAsync(entity);
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.BookDetails.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
