using AutoMapper;
using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Dapper.BLL.DTOs.BookDetailsDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.BLL.Services
{
    public class BookDetailsService : IBookDetailsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BookDetailsService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<Guid> CreateAsync(InsertDTO_BookDetails entity)
        {
            BookDetails newEntity = _mapper.Map<BookDetails>(entity); // Mapping without AutoMapper
            
            var id = await _uow.BookDetails.CreateAsync(newEntity);
            _uow.Commit();

            return id;
        }
        public async Task<GetDTO_BookDetails?> GetAsync(Guid id)
        {
            var entity = await _uow.BookDetails.GetByIdAsync(id);
            
            GetDTO_BookDetails? dto = _mapper.Map<GetDTO_BookDetails?>(entity); // Mapping without AutoMapper
            
            return dto;
        }
        public async Task<IEnumerable<GetDTO_BookDetails>> GetAllAsync()
        {
            // Use AutoMapper to project one collection onto another
            var list = _mapper
                .Map<IEnumerable<BookDetails>, IEnumerable<GetDTO_BookDetails>>(await _uow.BookDetails.GetAllAsync());
            return list;
        }
        public async Task UpdateAsync(UpdateDTO_BookDetails entity)
        {
            BookDetails upEntity = _mapper.Map<BookDetails>(entity); // Mapping without AutoMapper
            
            upEntity.DateCreated = (await _uow.BookDetails.GetByIdAsync(upEntity.BookDetailId))!.DateCreated;
            upEntity.DateUpdated = DateTime.UtcNow;
            
            await _uow.BookDetails.UpdateAsync(upEntity);
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
