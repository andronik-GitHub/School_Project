using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Dapper.BLL.DTO;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.BLL.Services
{
    public class PublisherService : IPublisherService
    {
        IUnitOfWork _uow;

        public PublisherService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CreateAsync(PublisherDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow.Publishers.CreateAsync(new Publisher
            {
                PublisherId = entity.PublisherId,
                Name = entity.Name,
                Location = entity.Location,
            });
            _uow.Commit();

            return id;
        }
        public async Task<PublisherDTO> GetAsync(Guid id)
        {
            var entity = await _uow.Publishers.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new PublisherDTO
            {
                PublisherId = entity.PublisherId,
                Name = entity.Name,
                Location = entity.Location,
            };
        }
        public async Task<IEnumerable<PublisherDTO>> GetAllAsync()
        {
            var list = await _uow.Publishers.GetAllAsync();
            var result = new List<PublisherDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new PublisherDTO
            {
                PublisherId = entity.PublisherId,
                Name = entity.Name,
                Location = entity.Location,
            }));

            return result;
        }
        public async Task UpdateAsync(PublisherDTO entity)
        {
            // Mapping without AutoMapper
            await _uow.Publishers.UpdateAsync(new Publisher
            {
                PublisherId = entity.PublisherId,
                Name = entity.Name,
                Location = entity.Location,
            });
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
