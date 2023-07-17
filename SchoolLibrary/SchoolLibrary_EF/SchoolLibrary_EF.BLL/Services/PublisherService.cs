using System.Dynamic;
using SchoolLibrary_EF.BLL.DTOs.PublisherDTOs;
using SchoolLibrary_EF.BLL.Mapping;
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


        public async Task<Guid> CreateAsync(InsertDTO_Publisher entity)
        {
            // Mapping with Mapster
            Publisher publisher = MappingFunctions.MapSourceToDestination<InsertDTO_Publisher, Publisher>(entity);

            var id = await _uow.Publishers.CreateAsync(publisher);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<GetDTO_Publisher>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<Publisher, GetDTO_Publisher>
                (await _uow.Publishers.GetAllAsync(parameters));
        }
        public async Task<GetDTO_Publisher?> GetAsync(Guid id)
        {
            Publisher? publisher = await _uow.Publishers.GetByIdAsync(id);

            GetDTO_Publisher? publisherDTO = MappingFunctions
                .MapSourceToDestination<Publisher?, GetDTO_Publisher?>(publisher); // Mapping with Mapster

            return publisherDTO;
        }
        public async Task UpdateAsync(UpdateDTO_Publisher entity)
        {
            // Mapping with Mapster
            Publisher publisher = MappingFunctions.MapSourceToDestination<UpdateDTO_Publisher, Publisher>(entity);

            await _uow.Publishers.UpdateAsync(publisher);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Publishers.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters)
        {
            return await _uow.Publishers.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters parameters)
        {
            return await _uow.Publishers.GetById_DataShaping_Async(id, parameters);
        }
    }
}
