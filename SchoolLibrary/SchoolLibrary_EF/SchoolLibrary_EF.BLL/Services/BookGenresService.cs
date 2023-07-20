using System.Dynamic;
using SchoolLibrary_EF.BLL.DTOs.BookGenreDTOs;
using SchoolLibrary_EF.BLL.Mapping;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;
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


        public async Task<(Guid, Guid)> CreateAsync(InsertDTO_BookGenres entity)
        {
            // Mapping with Mapster
            var bookGenres = MappingFunctions.MapSourceToDestination<InsertDTO_BookGenres, BookGenres>(entity);

            var checkFind = await _uow.BookGenres.GetByIdAsync((bookGenres.BookId, bookGenres.GenreId));
            if (checkFind != null) throw new Exception("Object with such values already exists in the database!");

            bookGenres.Book = default!;
            bookGenres.Genre = default!;

            var id = await _uow.BookGenres.CreateAsync(bookGenres);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<GetDTO_BookGenres>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<BookGenres, GetDTO_BookGenres>
                (await _uow.BookGenres.GetAllAsync(parameters));
        }
        public async Task<GetDTO_BookGenres?> GetAsync((Guid , Guid) key)
        {
            BookGenres? bookGenres = await _uow.BookGenres.GetByIdAsync(key);

            GetDTO_BookGenres? bookGenresDTO = MappingFunctions
                .MapSourceToDestination<BookGenres?, GetDTO_BookGenres?>(bookGenres); // Mapping with Mapster

            return bookGenresDTO;
        }
        public async Task UpdateAsync(UpdateDTO_BookGenres entity)
        {
            // Mapping with Mapster
            BookGenres bookGenres = MappingFunctions.MapSourceToDestination<UpdateDTO_BookGenres, BookGenres>(entity);

            await _uow.BookGenres.UpdateAsync(bookGenres);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync((Guid , Guid) key)
        {
            var entity = await _uow.BookGenres.GetByIdAsync(key);
            if (entity == null) throw new Exception($"BookGenres with id: [{key.Item1}]-[{key.Item2}] was not found");

            await _uow.BookGenres.DeleteAsync(key);
            await _uow.SaveChangesAsync();
        }

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters)
        {
            return await _uow.BookGenres.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async((Guid, Guid) key, BaseParameters parameters)
        {
            return await _uow.BookGenres.GetById_DataShaping_Async(key, parameters);
        }
    }
}
