using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        IUnitOfWork _uow;

        public AuthorService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CreateAsync(Author entity)
        {
            var id = await _uow.Authors.CreateAsync(entity);
            _uow.Commit();

            return id;
        }
        public async Task<Author> GetAsync(Guid id)
        {
            return await _uow.Authors.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _uow.Authors.GetAllAsync();
        }
        public async Task UpdateAsync(Author entity)
        {
            await _uow.Authors.UpdateAsync(entity);
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
