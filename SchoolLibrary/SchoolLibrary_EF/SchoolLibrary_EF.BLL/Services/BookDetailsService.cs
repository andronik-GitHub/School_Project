using System.Dynamic;
using SchoolLibrary_EF.BLL.DTOs.BookDetailsDTOs;
using SchoolLibrary_EF.BLL.Mapping;
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


        public async Task<Guid> CreateAsync(InsertDTO_BookDetails entity)
        {
            // Mapping with Mapster
            BookDetails bookDetails = MappingFunctions
                .MapSourceToDestination<InsertDTO_BookDetails, BookDetails>(entity);

            bookDetails.Book = default!;
            
            var id = await _uow.BookDetails.CreateAsync(bookDetails);
            await _uow.SaveChangesAsync();

            return id;
        }
        public async Task<IEnumerable<GetDTO_BookDetails>> GetAllAsync(BaseParameters parameters)
        {
            // Use Mapster to project one collection onto another
            return MappingFunctions.MapListSourceToDestination<BookDetails, GetDTO_BookDetails>
                (await _uow.BookDetails.GetAllAsync(parameters));
        }
        public async Task<GetDTO_BookDetails?> GetAsync(Guid id)
        {
            BookDetails? bookDetails = await _uow.BookDetails.GetByIdAsync(id);

            GetDTO_BookDetails? bookDetailsDTO = MappingFunctions
                .MapSourceToDestination<BookDetails?, GetDTO_BookDetails?>(bookDetails); // Mapping with Mapster

            return bookDetailsDTO;
        }
        public async Task UpdateAsync(UpdateDTO_BookDetails entity)
        {
            // Mapping with Mapster
            BookDetails bookDetails = MappingFunctions
                .MapSourceToDestination<UpdateDTO_BookDetails, BookDetails>(entity);

            await _uow.BookDetails.UpdateAsync(bookDetails);
            await _uow.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.BookDetails.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<PagedList<ExpandoObject>> GetAll_DataShaping_Async(BaseParameters parameters)
        {
            return await _uow.BookDetails.GetAll_DataShaping_Async(parameters);
        }
        public async Task<ExpandoObject?> GetById_DataShaping_Async(Guid key, BaseParameters parameters)
        {
            return await _uow.BookDetails.GetById_DataShaping_Async(key, parameters);
        }
    }
}
