using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Pagging;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.BLL.Services
{
    public class BookAuthorsService : IBookAuthorsService
    {
        private readonly IUnitOfWork _uow;

        public BookAuthorsService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<(Guid, Guid)?> CreateAsync(BookAuthorsDTO entity)
        {
            // We create a BookAuthors object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            var bookAuthors = MappingFunctions.MapSourceToDestination<BookAuthorsDTO, BookAuthors>(entity);

            await SeedingBookAuthorsObject(entity, bookAuthors);

            // Checking whether such an object does not already exist
            var checkFind = _uow.BookGenres.GetByIdAsync(bookAuthors.BookId, bookAuthors.AuthorId);
            if (checkFind != null) return null;

            var id = await _uow.BookAuthors.CreateAsync(bookAuthors.BookId, bookAuthors.AuthorId, bookAuthors);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<BookAuthorsDTO>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<BookAuthors, BookAuthorsDTO>
                (await _uow.BookAuthors.GetAllAsync(parameters));
        }
        public async Task<BookAuthorsDTO?> GetByIdAsync(Guid firstId, Guid secondId)
        {
            // Get entity from db
            BookAuthors? bookAuthors = await _uow.BookAuthors.GetByIdAsync(firstId, secondId);

            // We create a BookAuthors object and copy the values ​​of the properties
            // of the bookAuthors object into its properties (we perform mapping)
            BookAuthorsDTO? bookAuthorsDTO =
                // There may be no entity in the database,
                // exception catching must be implemented on the controller side
                bookAuthors == null ?
                null : MappingFunctions.MapSourceToDestination<BookAuthors, BookAuthorsDTO>(bookAuthors);

            return bookAuthorsDTO;
        }
        public async Task UpdateAsync(BookAuthorsDTO entity)
        {
            // We create a BookAuthors object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            BookAuthors bookAuthors = MappingFunctions.MapSourceToDestination<BookAuthorsDTO, BookAuthors>(entity);

            await SeedingBookAuthorsObject(entity, bookAuthors);

            await _uow.BookAuthors.UpdateAsync(bookAuthors);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid firstId, Guid secondId)
        {
            var entity = await _uow.BookAuthors.GetByIdAsync(firstId, secondId);
            if (entity == null) throw new Exception($"BookAuthors with id: [{firstId}]-[{secondId}] was not found");

            await _uow.BookAuthors.DeleteAsync(entity);
            await _uow.SaveChangesAsync();
        }


        public async Task<(Guid, Guid)> GetIdsToOjbect(BookAuthorsDTO entity)
        {
            // We create a BookAuthors object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            var obj = MappingFunctions.MapSourceToDestination<BookAuthorsDTO, BookAuthors>(entity);

            return await Task.Run(() => (obj.BookId, obj.AuthorId));
        }
        // For filling FK and objects
        private async Task SeedingBookAuthorsObject(BookAuthorsDTO entity, BookAuthors bookAuthors)
        {
            var book = (await _uow.Books.GetAllAsync<Guid>())
                .Where(book => book.Title == entity.BookTitle)
                .FirstOrDefault();
            var author = (await _uow.Authors.GetAllAsync<Guid>())
                .Where(author => $"{author.FirstName} {author.LastName}" == entity.AuthorFullName)
                .FirstOrDefault();


            if (book == null) throw new Exception("No book with this title was found");
            else
            {
                bookAuthors.BookId = book.BookId;
                bookAuthors.Book = book;
            }
            if (author == null) throw new Exception("No author with this name was found");
            else
            {
                bookAuthors.AuthorId = author.AuthorId;
                bookAuthors.Author = author;
            }
        }
    }
}
