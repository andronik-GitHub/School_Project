using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.AuthorFeatures.Queries.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthorFeatures.Queries.GetAuthorWithHighestAvgBookRating
{
    public class GetAuthorWithHighestAvgBookRatingQueryHandler 
        : IRequestHandler<GetAuthorWithHighestAvgBookRatingQuery, GetDTO_AuthorWithHighestAvgBookRating>
    {
        private readonly ISchoolLibraryContext _context;

        public GetAuthorWithHighestAvgBookRatingQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
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
        public async Task<GetDTO_AuthorWithHighestAvgBookRating> Handle
            (GetAuthorWithHighestAvgBookRatingQuery query, CancellationToken cancellationToken)
        {
            // Query
            var entity = await _context.Authors
                .AsNoTracking()
                .GroupJoin(
                    _context.BookAuthors
                        .AsNoTracking()
                        .Join(
                            _context.Books
                                .AsNoTracking()
                                .Join(
                                    _context.Reviews.AsNoTracking(),
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
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            // Mapping (Mapster)
            if (entity == null) throw new Exception("Entity from [Authors] not found");
            return MapsterFunctions.MapSourceToDestination
                <(string FirstName, string LastName, decimal AverageRating), GetDTO_AuthorWithHighestAvgBookRating>
                ((entity.FirstName, entity.LastName, entity.AverageRating));
        }
    }
}