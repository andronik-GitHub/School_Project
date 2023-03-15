using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.DTO;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class BookAuthorsService : IBookAuthorsService
    {
        IUnitOfWork _uow;

        public BookAuthorsService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Guid> CreateAsync(BookAuthorsDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow.BookAuthors.CreateAsync(new BookAuthors
            {
                BookId = entity.BookId,
                AuthorId = entity.AuthorId,
            });
            _uow.Commit();

            return id;
        }
        public async Task<BookAuthorsDTO> GetAsync(Guid id)
        {
            var entity = await _uow.BookAuthors.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new BookAuthorsDTO
            {
                BookId = entity.BookId,
                AuthorId = entity.AuthorId,
            };
        }
        public async Task<IEnumerable<BookAuthorsDTO>> GetAllAsync()
        {
            var list = await _uow.BookAuthors.GetAllAsync();
            var result = new List<BookAuthorsDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new BookAuthorsDTO
            {
                BookId = entity.BookId,
                AuthorId = entity.AuthorId,
            }));

            return result;
        }
        public async Task UpdateAsync(BookAuthorsDTO entity)
        {
            // Mapping without AutoMapper
            await _uow.BookAuthors.UpdateAsync(new BookAuthors
            {
                BookId = entity.BookId,
                AuthorId = entity.AuthorId,
            });
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.BookAuthors.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
