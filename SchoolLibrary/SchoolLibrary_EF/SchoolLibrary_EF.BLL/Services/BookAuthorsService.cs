using System.Dynamic;
using SchoolLibrary_EF.BLL.DTOs.BookAuthorDTOs;
using SchoolLibrary_EF.BLL.Mapping;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;
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


        public async Task<(Guid, Guid)> CreateAsync(InsertDTO_BookAuthors entity)
        {
            // Mapping with Mapster
            var bookAuthors = MappingFunctions.MapSourceToDestination<InsertDTO_BookAuthors, BookAuthors>(entity);

            var checkFind = await _uow.BookAuthors.GetByIdAsync((bookAuthors.BookId, bookAuthors.AuthorId));
            if (checkFind != null) throw new Exception("Object with such values already exists in the database!");

            var id = await _uow.BookAuthors.CreateAsync(bookAuthors);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<GetDTO_BookAuthors>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<BookAuthors, GetDTO_BookAuthors>
                (await _uow.BookAuthors.GetAllAsync(parameters));
        }
        public async Task<GetDTO_BookAuthors?> GetAsync((Guid, Guid) key)
        {
            BookAuthors? bookAuthors = await _uow.BookAuthors.GetByIdAsync(key);

            GetDTO_BookAuthors? bookAuthorsDTO = MappingFunctions
                .MapSourceToDestination<BookAuthors?, GetDTO_BookAuthors?>(bookAuthors); // Mapping with Mapster

            return bookAuthorsDTO;
        }
        public async Task UpdateAsync(UpdateDTO_BookAuthors entity)
        {
            BookAuthors bookAuthors = MappingFunctions
                .MapSourceToDestination<UpdateDTO_BookAuthors, BookAuthors>(entity);

            await _uow.BookAuthors.UpdateAsync(bookAuthors);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync((Guid, Guid) key)
        {
            var entity = await _uow.BookAuthors.GetByIdAsync(key);
            if (entity == null) throw new Exception($"Entity with id: [{key.Item1}]-[{key.Item2}] was not found");

            await _uow.BookAuthors.DeleteAsync(key);
            await _uow.SaveChangesAsync();
        }

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters)
        {
            return await _uow.BookAuthors.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async((Guid, Guid) key, BaseParameters parameters)
        {
            return await _uow.BookAuthors.GetById_DataShaping_Async(key, parameters);
        }
    }
}
