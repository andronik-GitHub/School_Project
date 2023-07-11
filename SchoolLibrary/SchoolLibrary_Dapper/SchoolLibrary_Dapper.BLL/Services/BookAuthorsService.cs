using AutoMapper;
using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Dapper.BLL.DTOs.BookAuthorDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.BLL.Services
{
    public class BookAuthorsService : IBookAuthorsService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BookAuthorsService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<(Guid, Guid)> CreateAsync(InsertDTO_BookAuthors entity)
        {
            BookAuthors newEntity = _mapper.Map<BookAuthors>(entity); // Mapping without AutoMapper

            var id = await _uow.BookAuthors.CreateAsync(newEntity);
            _uow.Commit();

            return id;
        }
        public async Task<GetDTO_BookAuthors?> GetAsync(Guid BookId, Guid AuthorId)
        {
            var entity = await _uow.BookAuthors.GetByIdsAsync(BookId, AuthorId);
            
            GetDTO_BookAuthors? dto = _mapper.Map<GetDTO_BookAuthors?>(entity); // Mapping without AutoMapper
            
            return dto;
        }
        public async Task<GetDTOInclude_BookAuthors?> Include_GetAsync(Guid BookId, Guid AuthorId)
        {
            var entity = await _uow.BookAuthors.Include_GetByIdsAsync(BookId, AuthorId);
            
            GetDTOInclude_BookAuthors? dto = _mapper.Map<GetDTOInclude_BookAuthors?>(entity); // Mapping without AutoMapper

            return dto;
        }
        public async Task<IEnumerable<GetDTO_BookAuthors>> GetAllAsync()
        {
            var list = _mapper.Map<IEnumerable<BookAuthors>, IEnumerable<GetDTO_BookAuthors>>
                (await _uow.BookAuthors.GetAllAsync());

            return list;
        }
        public async Task<IEnumerable<GetDTOInclude_BookAuthors>> Include_GetAllAsync()
        {
            var list = _mapper.Map<IEnumerable<(Book, Author)>, IEnumerable<GetDTOInclude_BookAuthors>>
                (await _uow.BookAuthors.Include_GetAllAsync());

            return list;
        }
        public async Task UpdateAsync(UpdateDTO_BookAuthors entity)
        {
            BookAuthors upEntity = _mapper.Map<BookAuthors>(entity); // Mapping without AutoMapper
            
            upEntity.DateCreated = 
                (await _uow.BookAuthors.GetByIdsAsync(upEntity.BookId, upEntity.AuthorId))!.DateCreated;
            upEntity.DateUpdated = DateTime.UtcNow;
            
            await _uow.BookAuthors.UpdateAsync(upEntity);
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid BookId, Guid AuthorId)
        {
            await _uow.BookAuthors.DeleteAsync(BookId, AuthorId);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
