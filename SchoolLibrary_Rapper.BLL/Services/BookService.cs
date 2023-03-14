using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using SchoolLibrary_Rapper.BLL.Services.Consracts;

namespace SchoolLibrary_Rapper.BLL.Services
{
    public class BookService : IBookService
    {
        IUnitOfWork unitOfWork { get; set; }

        public BookService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task<Guid> CreateAsync(Book entity)
        {
            var id = await unitOfWork.Books.CreateAsync(entity);
            unitOfWork.Commit();

            return id;
        }
        public async Task<Book> GetAsync(Guid id)
        {
            return await unitOfWork.Books.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await unitOfWork.Books.GetAllAsync();
        }
        public async Task UpdateAsync(Book entity)
        {
            await unitOfWork.Books.UpdateAsync(entity);
            unitOfWork.Commit();
        }
        public async Task DeleteAsync(Guid id)
        {
            await unitOfWork.Books.DeleteAsync(id);
            unitOfWork.Commit();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
