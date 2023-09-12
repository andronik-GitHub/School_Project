using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Common.Pagging;
using Application.Features.BookFeatures.Queries.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookFeatures.Queries.GetBooksWithoutReviews
{
    public class GetBooksWithoutReviewsQueryHandler 
        : IRequestHandler<GetBooksWithoutReviewsQuery, IEnumerable<GetDTO_BookWithoutReviews>>
    {
        private readonly ISchoolLibraryContext _context;

        public GetBooksWithoutReviewsQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }

        
        /*  Find books that don't have reviews
         
            SELECT B.Title AS BookTitle
            FROM Books B
            LEFT JOIN Reviews R ON B.BookId = R.BookId
            WHERE R.ReviewId IS NULL;
            
         */
        public async Task<IEnumerable<GetDTO_BookWithoutReviews>> Handle
            (GetBooksWithoutReviewsQuery query, CancellationToken cancellationToken)
        {
            // Query
            var list = await _context.Books
                .AsNoTracking()
                .GroupJoin(
                    _context.Reviews.AsNoTracking(),
                    b => b.BookId,
                    r => r.BookId,
                    (b, r) => new
                    {
                        Book = b,
                        ReviewsCount = r.Count()
                    })
                .Where(result => result.ReviewsCount <= 0)
                .ToListAsync(cancellationToken: cancellationToken);
            
            // Paging
            var pagingList = PagedList<(Guid BookId, string BookTitle)>
                .ToPagedList(
                    list.Select(item => (item.Book.BookId, item.Book.Title)).AsQueryable(), 
                    query._parameters.PageNumber, 
                    query._parameters.PageSize);
            
            // Mapping (Mapster)
            return MapsterFunctions.MapListSourceToDestination
                <(Guid BookId, string BookTitle), GetDTO_BookWithoutReviews>
                (pagingList);
        }
    }
}