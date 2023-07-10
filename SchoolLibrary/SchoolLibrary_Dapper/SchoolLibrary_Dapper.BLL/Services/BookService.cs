using AutoMapper;
using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Dapper.BLL.DTOs.BookDTOs;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<Guid> CreateAsync(InsertDTO_Book entity)
        {
            Book newEntity = _mapper.Map<Book>(entity); // Mapping without AutoMapper
            
            var id = await _uow.Books.CreateAsync(newEntity);
            _uow.Commit();

            return id;
        }
        public async Task<GetDTO_Book?> GetAsync(Guid id)
        {
            var entity = await _uow.Books.GetByIdAsync(id);
            
            GetDTO_Book? dto = _mapper.Map<GetDTO_Book?>(entity); // Mapping without AutoMapper

            return dto;
        }
        public async Task<IEnumerable<GetDTO_Book>> GetAllAsync()
        {
            // Use AutoMapper to project one collection onto another
            var list = _mapper.Map<IEnumerable<Book>, IEnumerable<GetDTO_Book>>(await _uow.Books.GetAllAsync());
            return list;
        }
        public async Task UpdateAsync(UpdateDTO_Book entity)
        {
            Book upEntity = _mapper.Map<Book>(entity); // Mapping without AutoMapper
            
            await _uow.Books.UpdateAsync(upEntity);
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Books.DeleteAsync(id);
            _uow.Commit();
        }

        public async Task<IEnumerable<GetDTO_BookWithAuthorsAndGenres>> GetBooksWithAuthorsAndGenresAsync()
        {
            // Use AutoMapper to project one collection onto another
            var list = _mapper.Map<IEnumerable<(Book, Author, Genre)>, IEnumerable<GetDTO_BookWithAuthorsAndGenres>>
                (await _uow.Books.GetBooksWithAuthorsAndGenresAsync());
            
            return list;
        }
        public async Task<GetDTO_BookWithAuthorsAndGenres?> GetBookWithAuthorAndGenreAsync(Guid id)
        {
            var entity = await _uow.Books.GetByIdBookWithAuthorAndGenreAsync(id);
            
            GetDTO_BookWithAuthorsAndGenres? dto = _mapper.Map<GetDTO_BookWithAuthorsAndGenres?>(entity); // Mapping without AutoMapper

            return dto;
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
