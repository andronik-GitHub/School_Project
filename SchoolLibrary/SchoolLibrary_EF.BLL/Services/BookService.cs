using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _uow;

        public BookService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<Guid> CreateAsync(BookDTO entity)
        {
            // We create a Book object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Book book = MappingFunctions.MapSourceToDestination<BookDTO, Book>(entity);

            var id = await _uow.Books.CreateAsync(book);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<Book, BookDTO>
                (await _uow.Books.GetAllAsync());
        }
        public async Task<BookDTO?> GetAsync(Guid id)
        {
            // Get entity from db
            Book? book = await _uow.Books.GetByIdAsync(id);

            // We create a BookDTO object and copy the values ​​of the properties
            // of the book object into its properties (we perform mapping)
            BookDTO? bookDTO =
                // There may be no entity in the database,
                // exception catching must be implemented on the controller side
                book == null ?
                null : MappingFunctions.MapSourceToDestination<Book, BookDTO>(book);

            return bookDTO;
        }
        public async Task UpdateAsync(BookDTO entity)
        {
            // We create a Book object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            Book book = MappingFunctions.MapSourceToDestination<BookDTO, Book>(entity);

            await _uow.Books.UpdateAsync(book);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Books.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
