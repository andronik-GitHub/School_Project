using AutoMapper;
using SchoolLibrary_Dapper.BLL.DTOs.BookGenreDTOs;
using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.BLL.Services
{
    public class BookGenresService : IBookGenresService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BookGenresService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<(Guid, Guid)> CreateAsync(InsertDTO_BookGenres entity)
        {
            BookGenres newEntity = _mapper.Map<BookGenres>(entity); // Mapping without AutoMapper

            var id = await _uow.BookGenres.CreateAsync(newEntity);
            _uow.Commit();

            return id;
        }
        public async Task<GetDTO_BookGenres?> GetAsync(Guid BookId, Guid GenreId)
        {
            var entity = await _uow.BookGenres.GetByIdsAsync(BookId, GenreId);
            
            GetDTO_BookGenres? dto = _mapper.Map<GetDTO_BookGenres?>(entity); // Mapping without AutoMapper
            
            return dto;
        }
        public async Task<GetDTOInclude_BookGenres?> Include_GetAsync(Guid BookId, Guid GenreId)
        {
            var entity = await _uow.BookGenres.Include_GetByIdsAsync(BookId, GenreId);
            
            GetDTOInclude_BookGenres? dto = _mapper.Map<GetDTOInclude_BookGenres?>(entity); // Mapping without AutoMapper

            return dto;
        }
        public async Task<IEnumerable<GetDTO_BookGenres>> GetAllAsync()
        {
            var list = _mapper.Map<IEnumerable<BookGenres>, IEnumerable<GetDTO_BookGenres>>
                (await _uow.BookGenres.GetAllAsync());

            return list;
        }
        public async Task<IEnumerable<GetDTOInclude_BookGenres>> Include_GetAllAsync()
        {
            var list = _mapper.Map<IEnumerable<(Book, Genre)>, IEnumerable<GetDTOInclude_BookGenres>>
                (await _uow.BookGenres.Include_GetAllAsync());

            return list;
        }
        public async Task UpdateAsync(UpdateDTO_BookGenres entity)
        {
            BookGenres upEntity = _mapper.Map<BookGenres>(entity); // Mapping without AutoMapper

            await _uow.BookGenres.UpdateAsync(upEntity);
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid BookId, Guid GenreId)
        {
            await _uow.BookGenres.DeleteAsync(BookId, GenreId);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
