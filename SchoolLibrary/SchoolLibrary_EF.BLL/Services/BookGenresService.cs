using SchoolLibrary_EF.API.Mapping.Configurations;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Pagging.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;

namespace SchoolLibrary_EF.BLL.Services
{
    public class BookGenresService : IBookGenresService
    {
        private readonly IUnitOfWork _uow;

        public BookGenresService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<(Guid, Guid)?> CreateAsync(BookGenresDTO entity)
        {
            // We create a BookGenres object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            var bookGenres = MappingFunctions.MapSourceToDestination<BookGenresDTO, BookGenres>(entity);

            await SeedingBookGenresObject(entity, bookGenres);

            // Checking whether such an object does not already exist
            var checkFind = _uow.BookGenres.GetByIdAsync(bookGenres.BookId, bookGenres.GenreId);
            if (checkFind != null) return null;

            var id = await _uow.BookGenres.CreateAsync(bookGenres.BookId, bookGenres.GenreId, bookGenres);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<BookGenresDTO>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<BookGenres, BookGenresDTO>
                (await _uow.BookGenres.GetAllAsync(parameters));
        }
        public async Task<BookGenresDTO?> GetByIdAsync(Guid firstId, Guid secondId)
        {
            // Get entity from db
            BookGenres? bookGenres = await _uow.BookGenres.GetByIdAsync(firstId, secondId);

            // We create a BookGenresDTO object and copy the values ​​of the properties
            // of the bookGenres object into its properties (we perform mapping)
            BookGenresDTO? bookGenresDTO =
                // There may be no entity in the database,
                // exception catching must be implemented on the controller side
                bookGenres == null ?
                null : MappingFunctions.MapSourceToDestination<BookGenres, BookGenresDTO>(bookGenres);

            return bookGenresDTO;
        }
        public async Task UpdateAsync(BookGenresDTO entity)
        {
            // We create a BookGenres object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            BookGenres bookGenres = MappingFunctions.MapSourceToDestination<BookGenresDTO, BookGenres>(entity);

            await SeedingBookGenresObject(entity, bookGenres);

            await _uow.BookGenres.UpdateAsync(bookGenres);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid firstId, Guid secondId)
        {
            var entity = await _uow.BookGenres.GetByIdAsync(firstId, secondId);
            if (entity == null) throw new Exception($"BookGenres with id: [{firstId}]-[{secondId}] was not found");

            await _uow.BookGenres.DeleteAsync(entity);
            await _uow.SaveChangesAsync();
        }

        public async Task<(Guid, Guid)> GetIdsToOjbect(BookGenresDTO entity)
        {
            // We create a BookGenres object and copy the values ​​of the properties
            // of the entity object into its properties (we perform mapping)
            var obj = MappingFunctions.MapSourceToDestination<BookGenresDTO, BookGenres>(entity);

            return await Task.Run(() => (obj.BookId, obj.GenreId));
        }
        // For filling FK and objects
        private async Task SeedingBookGenresObject(BookGenresDTO entity, BookGenres bookGenres)
        {
            var book = (await _uow.Books.GetAllAsync())
                .ToList()
                .Where(book => book.Title == entity.BookTitle)
                .FirstOrDefault();
            var genre = (await _uow.Genres.GetAllAsync())
                .ToList()
                .Where(genre => genre.Name == entity.GenreName)
                .FirstOrDefault();


            if (book == null) throw new Exception("No book with this title was found");
            else
            {
                bookGenres.BookId = book.BookId;
                bookGenres.Book = book;
            }
            if (genre == null) throw new Exception("No genre with this name was found");
            else
            {
                bookGenres.GenreId = genre.GenreId;
                bookGenres.Genre = genre;
            }
        }
    }
}
