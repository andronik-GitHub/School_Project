using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class PublisherService : IPublisherService
    {
        IUnitOfWork _uow;

        public PublisherService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CreateAsync(Publisher entity)
        {
            var id = await _uow.Publishers.CreateAsync(entity);
            _uow.Commit();

            return id;
        }
        public async Task<Publisher> GetAsync(Guid id)
        {
            return await _uow.Publishers.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            return await _uow.Publishers.GetAllAsync();
        }
        public async Task UpdateAsync(Publisher entity)
        {
            await _uow.Publishers.UpdateAsync(entity);
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Publishers.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
