using System.Dynamic;
using AutoMapper;
using SchoolLibrary_EF.BLL.DTOs.AuthorDTOs;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private IUnitOfWork _uow { get; set; }
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(InsertDTO_Author entity)
        {
            Author author = _mapper.Map<Author>(entity); // Mapping with AutoMapper

            var id = await _uow.Authors.CreateAsync(author);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<GetDTO_Author>> GetAllAsync(BaseParameters parameters)
        {
            // Use AutoMapper to project one collection onto another
            return _mapper.Map<IEnumerable<Author>, IEnumerable<GetDTO_Author>>
                (await _uow.Authors.GetAllAsync(parameters));
        }
        public async Task<GetDTO_Author?> GetAsync(Guid id)
        {
            Author? author = await _uow.Authors.GetByIdAsync(id);
            GetDTO_Author? authorDTO = _mapper.Map<GetDTO_Author?>(author); // Mapping with AutoMapper
            return authorDTO;
        }
        public async Task UpdateAsync(UpdateDTO_Author entity)
        {
            Author author = _mapper.Map<Author>(entity); // Mapping with AutoMapper
            await _uow.Authors.UpdateAsync(author);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Authors.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }
        

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters)
        {
            return await _uow.Authors.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters parameters)
        {
            return await _uow.Authors.GetById_DataShaping_Async(id, parameters);
        }
    }
}
