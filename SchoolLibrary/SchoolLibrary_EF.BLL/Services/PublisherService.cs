using System.Dynamic;
using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.BLL.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _uow;

        public PublisherService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }


        public async Task<Guid> CreateAsync(PublisherDTO entity)
        {
            // We create a Publisher object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Publisher publisher = MappingFunctions.MapSourceToDestination<PublisherDTO, Publisher>(entity);

            var id = await _uow.Publishers.CreateAsync(publisher);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<PublisherDTO>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<Publisher, PublisherDTO>
                (await _uow.Publishers.GetAllAsync(parameters));
        }
        public async Task<PublisherDTO?> GetAsync(Guid id)
        {
            // Get entity from db
            Publisher? publisher = await _uow.Publishers.GetByIdAsync(id);

            // We create a PublisherDTO object and copy the values ​​of the properties
            // of the genre object into its properties (we perform mapping)
            PublisherDTO? publisherDTO =
                // There may be no entity in the database,
                // exception catching must be implemented on the controller side
                publisher == null ?
                null : MappingFunctions.MapSourceToDestination<Publisher, PublisherDTO>(publisher);

            return publisherDTO;
        }
        public async Task UpdateAsync(PublisherDTO entity)
        {
            // We create a Publisher object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Publisher publisher = MappingFunctions.MapSourceToDestination<PublisherDTO, Publisher>(entity);

            await _uow.Publishers.UpdateAsync(publisher);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Publishers.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null)
        {
            return await _uow.Publishers.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters? parameters = null)
        {
            return await _uow.Publishers.GetById_DataShaping_Async(id, parameters);
        }
    }
}
