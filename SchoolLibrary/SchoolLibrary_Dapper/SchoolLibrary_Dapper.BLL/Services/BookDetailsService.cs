using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Dapper.BLL.DTO;
using SchoolLibrary_Dapper.BLL.Services.Consracts;

namespace SchoolLibrary_Dapper.BLL.Services
{
    public class BookDetailsService : IBookDetailsService
    {
        IUnitOfWork _uow;

        public BookDetailsService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<Guid> CreateAsync(BookDetailsDTO entity)
        {
            // Mapping without AutoMapper
            var id = await _uow.BookDetails.CreateAsync(new BookDetails
            {
                BookDetailId = entity.BookDetailId,
                BookId = entity.BookId,
                Pages = entity.Pages,
                Language = entity.Language,
                Format = entity.Format,
            });
            _uow.Commit();

            return id;
        }
        public async Task<BookDetailsDTO> GetAsync(Guid id)
        {
            var entity = await _uow.BookDetails.GetByIdAsync(id);

            // Mapping without AutoMapper
            return new BookDetailsDTO
            {
                BookDetailId = entity.BookDetailId,
                BookId = entity.BookId,
                Pages = entity.Pages,
                Language = entity.Language,
                Format = entity.Format,
            };
        }
        public async Task<IEnumerable<BookDetailsDTO>> GetAllAsync()
        {
            var list = await _uow.BookDetails.GetAllAsync();
            var result = new List<BookDetailsDTO>();

            // Mapping without AutoMapper
            list.ToList().ForEach(entity => result.Add(new BookDetailsDTO
            {
                BookDetailId = entity.BookDetailId,
                BookId = entity.BookId,
                Pages = entity.Pages,
                Language = entity.Language,
                Format = entity.Format,
            }));

            return result;
        }
        public async Task UpdateAsync(BookDetailsDTO entity)
        {
            // Mapping without AutoMapper
            await _uow.BookDetails.UpdateAsync(new BookDetails
            {
                BookDetailId = entity.BookDetailId,
                BookId = entity.BookId,
                Pages = entity.Pages,
                Language = entity.Language,
                Format = entity.Format,
            });
            _uow.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await _uow.BookDetails.DeleteAsync(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
    }
}
