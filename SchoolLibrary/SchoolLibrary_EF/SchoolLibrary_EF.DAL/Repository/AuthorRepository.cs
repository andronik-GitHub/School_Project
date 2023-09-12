using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Entities;
using SchoolLibrary_EF.DAL.Repository.Contracts;
using SchoolLibrary_EF.DAL.Data;
using SchoolLibrary_EF.DAL.Helper.Contracts;
using SchoolLibrary_EF.DAL.Helpers.Contracts;

namespace SchoolLibrary_EF.DAL.Repository
{
    public class AuthorRepository : GenericRepository<Author> , IAuthorRepository
    {
        public AuthorRepository(
            SchoolLibraryContext dbContext,
            IDataShaper<Author> dataShaper,
            ISortHelper<Author> sortHelper)
            : base(dbContext, dataShaper, sortHelper)
        {
        }

        /* Find the author with the highest average book rating
         
            SELECT TOP(1) A.FirstName, A.LastName, AVG(R.Rating) AS AverageRating
            FROM Authors A
            JOIN BookAuthors BA ON A.AuthorId = BA.AuthorId
            JOIN Books B ON BA.BookId = B.BookId
            LEFT JOIN Reviews R ON B.BookId = R.BookId
            GROUP BY A.AuthorId, A.FirstName, A.LastName
            ORDER BY AVG(R.Rating) DESC
         
         */
        public async Task<(string FirstName, string LastName, decimal AverageRating)> 
            GetAuthorWithHighestAvgBookRatingAsync()
        {
            var result = await entities
                .AsNoTracking()
                .GroupJoin(
                    dbContext.BookAuthors
                        .AsNoTracking()
                        .Join(
                            dbContext.Books
                                .AsNoTracking()
                                .Join(
                                    dbContext.Reviews.AsNoTracking(),
                                    b => b.BookId,
                                    r => r.BookId,
                                    (b, r) => new
                                    {
                                        b.BookId,
                                        r.Rating
                                    }),
                            ba => ba.BookId,
                            b => b.BookId,
                            (ba, b) => new
                            {  
                                ba.AuthorId,
                                b.Rating
                            }), 
                    a => a.AuthorId, 
                    ba => ba.AuthorId, 
                    (a, ba) => new
                    {
                        a.AuthorId,
                        a.FirstName,
                        a.LastName,
                        AverageRating = ba.Average(result => result.Rating)
                    })
                .OrderByDescending(result => result.AverageRating)
                .FirstOrDefaultAsync();

            if (result == null) throw new Exception($"Entity from [{nameof(Author) + "s"}] not found");
            return (result.FirstName, result.LastName, result.AverageRating);
        }
    }
}
