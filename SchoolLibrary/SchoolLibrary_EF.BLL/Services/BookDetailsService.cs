using System.Dynamic;
using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;
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

            
            await SeedingBookDetailsObject(entity, bookDetails);

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

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters? parameters = null)
        {
            return await _uow.BookDetails.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters? parameters = null)
        {
            return await _uow.BookDetails.GetById_DataShaping_Async(id, parameters);
        }
        
        private async Task SeedingBookDetailsObject(BookDetailsDTO entity, BookDetails bookDetails)
        {
            var bdList = await _uow.BookDetails.GetAllAsync();
            var book = (await _uow.Books.GetAllAsync()).FirstOrDefault(book =>
                    book.Title == entity.BookTitle && bdList.All(bd => bd.BookId != book.BookId));

            if (book == null) throw new Exception
                    ("No book with this title was found, or details of this book are already in the database!");
            
            bookDetails.Book.ISBN = book.ISBN;
            bookDetails.Book.PublisherId = book.PublisherId;
        }
    }
}
