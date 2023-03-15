using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.DTO;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class BookGenresService : IBookGenresService
    {
        IUnitOfWork _uow;

        public BookGenresService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CreateAsync(BookGenresDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow.BookGenres.CreateAsync(new BookGenres
            {
                BookId = entity.BookId,
                GenreId = entity.GenreId,
            });
            _uow.Commit();

            return id;
        }
        public async Task<BookGenresDTO> GetAsync(Guid id)
        {
            var entity = await _uow.BookGenres.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new BookGenresDTO
            {
                BookId = entity.BookId,
                GenreId = entity.GenreId,
            };
        }
        public async Task<IEnumerable<BookGenresDTO>> GetAllAsync()
        {
            var list = await _uow.BookGenres.GetAllAsync();
            var result = new List<BookGenresDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new BookGenresDTO
            {
                BookId = entity.BookId,
                GenreId = entity.GenreId,
            }));

            return result;
        }
        public async Task UpdateAsync(BookGenresDTO entity)
        {
            // Mapping without AutoMapper
            await _uow.BookGenres.UpdateAsync(new BookGenres
            {
                BookId = entity.BookId,
                GenreId = entity.GenreId,
            });
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.BookGenres.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
