using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.DTO;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class BookService : IBookService
    {
        IUnitOfWork _uow { get; set; }

        public BookService(IUnitOfWork unitOfWork)
        {
            this._uow = unitOfWork;
        }


        public async Task<Guid> CreateAsync(BookDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow.Books.CreateAsync(new Book
            {
                BookId = entity.BookId,
                Title = entity.Title,
                PublishingYear = entity.PublishingYear,
                PublisherId = entity.PublisherId,
                ISBN = entity.ISBN,
            });
            _uow.Commit();

            return id;
        }
        public async Task<BookDTO> GetAsync(Guid id)
        {
            var entity = await _uow.Books.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new BookDTO
            {
                BookId = entity.BookId,
                Title = entity.Title,
                PublishingYear = entity.PublishingYear,
                PublisherId = entity.PublisherId,
                ISBN = entity.ISBN,
            };
        }
        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            var list = await _uow.Books.GetAllAsync();
            var result = new List<BookDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new BookDTO
            {
                BookId = entity.BookId,
                Title = entity.Title,
                PublishingYear = entity.PublishingYear,
                PublisherId = entity.PublisherId,
                ISBN = entity.ISBN,
            }));

            return result;
        }
        public async Task UpdateAsync(BookDTO entity)
        {
            // Mapping without AutoMapper
            await _uow.Books.UpdateAsync(new Book
            {
                BookId = entity.BookId,
                Title = entity.Title,
                PublishingYear = entity.PublishingYear,
                PublisherId = entity.PublisherId,
                ISBN = entity.ISBN,
            });
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Books.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
