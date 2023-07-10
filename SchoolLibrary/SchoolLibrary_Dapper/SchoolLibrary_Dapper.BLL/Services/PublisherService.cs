using AutoMapper;
using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Dapper.BLL.DTOs.PublisherDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.BLL.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PublisherService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(InsertDTO_Publisher entity)
        {
            Publisher newEntity = _mapper.Map<Publisher>(entity); // Mapping without AutoMapper

            var id = await _uow.Publishers.CreateAsync(newEntity);
            _uow.Commit();

            return id;
        }
        public async Task<GetDTO_Publisher?> GetAsync(Guid id)
        {
            var entity = await _uow.Publishers.GetByIdAsync(id);

            GetDTO_Publisher? dto = _mapper.Map<GetDTO_Publisher?>(entity); // Mapping without AutoMapper
            
            return dto;
        }
        public async Task<IEnumerable<GetDTO_Publisher>> GetAllAsync()
        {
            // Use AutoMapper to project one collection onto another
            var list = _mapper
                .Map<IEnumerable<Publisher>, IEnumerable<GetDTO_Publisher>>(await _uow.Publishers.GetAllAsync());
            return list;
        }
        public async Task UpdateAsync(UpdateDTO_Publisher entity)
        {
            Publisher upEntity = _mapper.Map<Publisher>(entity); // Mapping without AutoMapper

            await _uow.Publishers.UpdateAsync(upEntity);
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
