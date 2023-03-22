using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Pagging;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.BLL.Services
{
    public class BookDetailsService : IBookDetailsService
    {
        private readonly IUnitOfWork _uow;

        public BookDetailsService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<Guid> CreateAsync(BookDetailsDTO entity)
        {
            // We create a BookDetails object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            BookDetails bookDetails = MappingFunctions.MapSourceToDestination<BookDetailsDTO, BookDetails>(entity);


            var bdList = (await _uow.BookDetails.GetAllAsync(new BookDetailsParameters())).ToList();
            // Finding a book with the same title to pre-fill data about this book
            var book = (await _uow.Books.GetAllAsync(new BookParameters()))
                .ToList()
                .Where(
                    book => book.Title == entity.BookTitle &&
                    !bdList.Any(bd => bd.BookId == book.BookId)
                )
                .FirstOrDefault();

            if (book == null)
                throw new Exception
                    ("No book with this title was found, or details of this book are already in the database!");
            else
            {
                bookDetails.Book.ISBN = book.ISBN;
                bookDetails.Book.PublisherId = book.PublisherId;
            }

            var id = await _uow.BookDetails.CreateAsync(bookDetails);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<BookDetailsDTO>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<BookDetails, BookDetailsDTO>
                (await _uow.BookDetails.GetAllAsync(parameters));
        }
        public async Task<BookDetailsDTO?> GetAsync(Guid id)
        {
            // Get entity from db
            BookDetails? bookDetails = await _uow.BookDetails.GetByIdAsync(id);

            // We create a BookDetailsDTO object and copy the values ​​of the properties
            // of the bookDetails object into its properties (we perform mapping)
            BookDetailsDTO? bookDetailsDTO =
                // There may be no entity in the database,
                // exception catching must be implemented on the controller side
                bookDetails == null ?
                null : MappingFunctions.MapSourceToDestination<BookDetails, BookDetailsDTO>(bookDetails);

            return bookDetailsDTO;
        }
        public async Task UpdateAsync(BookDetailsDTO entity)
        {
            // We create a BookDetails object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            BookDetails bookDetails = MappingFunctions.MapSourceToDestination<BookDetailsDTO, BookDetails>(entity);

            await _uow.BookDetails.UpdateAsync(bookDetails);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.BookDetails.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }
    }
}
