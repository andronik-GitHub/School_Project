using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repositories.Contracts;
using SchoolLibrary_EF.DAL.Repository;
using SchoolLibrary_EF.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace SchoolLibrary_EF.DAL.Repositories
{
    public class BookDetailsRepository : GenericRepository<BookDetails>, IBookDetailsRepository
    {
        public BookDetailsRepository(SchoolLibraryContext dbContext)
            : base(dbContext)
        {
        }


        public override async Task<Guid> CreateAsync(BookDetails bookDetails)
        {
            await entities.AddAsync(bookDetails);

            return bookDetails.BookDetailId;
        }
        public override async Task<IEnumerable<BookDetails>> GetAllAsync()
        {
            entities.ToList().ForEach(bookD =>
                dbContext.BookDetails.Include(bd => bd.Book).FirstOrDefault(b => b.BookId == bookD.BookId));

            return await entities.ToListAsync();
        }
        public override async Task<BookDetails?> GetByIdAsync(Guid id)
        {
            return await entities.Include(bd => bd.Book).FirstOrDefaultAsync(bd => bd.BookDetailId == id);
        }
    }
}
