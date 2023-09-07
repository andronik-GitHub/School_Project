using System.Dynamic;
using SchoolLibrary_EF.BLL.DTOs.BookDTOs;
using SchoolLibrary_EF.BLL.Mapping;
using SchoolLibrary_EF.BLL.Services.Contracts;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Paging;
using SchoolLibrary_EF.DAL.Paging.Entities;
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


        public async Task<Guid> CreateAsync(InsertDTO_Book entity)
        {
            Book book = MappingFunctions.MapSourceToDestination<InsertDTO_Book, Book>(entity); // Mapping with Mapster

            book.Publisher = default!;
            
            var id = await _uow.Books.CreateAsync(book);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<GetDTO_Book>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<Book, GetDTO_Book>
                (await _uow.Books.GetAllAsync(parameters));
        }
        public async Task<GetDTO_Book?> GetAsync(Guid id)
        {
            Book? book = await _uow.Books.GetByIdAsync(id);
            
            GetDTO_Book? bookDTO = MappingFunctions
                .MapSourceToDestination<Book?, GetDTO_Book?>(book); // Mapping with Mapster

            return bookDTO;
        }
        public async Task UpdateAsync(UpdateDTO_Book entity)
        {
            // Mapping with Mapster
            Book book = MappingFunctions.MapSourceToDestination<UpdateDTO_Book, Book>(entity);
            
            await _uow.Books.UpdateAsync(book);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.Books.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters)
        {
            return await _uow.Books.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(Guid id, BaseParameters parameters)
        {
            return await _uow.Books.GetById_DataShaping_Async(id, parameters);
        }


        public async Task<IEnumerable<GetDTO_AvgRatingBook>> GetAvgRatingForBookAsync(BookParameters parameters)
        {
            return MappingFunctions
                .MapListSourceToDestination<(string BookTitle, decimal? Average), GetDTO_AvgRatingBook>
                    (await _uow.Books.GetAvgRatingForBookAsync(parameters));
        }
    }
}
