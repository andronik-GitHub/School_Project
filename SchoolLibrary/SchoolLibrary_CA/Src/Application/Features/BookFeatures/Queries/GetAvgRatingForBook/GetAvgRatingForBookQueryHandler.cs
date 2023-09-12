using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Common.Pagging;
using Application.Features.BookFeatures.Queries.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookFeatures.Queries.GetAvgRatingForBook
{
    public class GetAvgRatingForBookQueryHandler 
        : IRequestHandler<GetAvgRatingForBookQuery, IEnumerable<GetDTO_AvgRatingBook>>
    {
        private readonly ISchoolLibraryContext _context;

        public GetAvgRatingForBookQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        /*  Average rating for each book
         
            SELECT B.Title AS BookTitle, AVG(R.Rating) AS AverageRating
            FROM Books B
            LEFT JOIN Reviews R ON B.BookId = R.BookId
            GROUP BY B.BookId, B.Title;
            
         */
        public async Task<IEnumerable<GetDTO_AvgRatingBook>> Handle
            (GetAvgRatingForBookQuery query, CancellationToken cancellationToken)
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
                        BookTitle = b.Title, 
                        Average = r.DefaultIfEmpty().Average(review => review != null ? review.Rating : null)
                    })
                .ToListAsync(cancellationToken: cancellationToken);
            
            // Paging
            var pagingList = PagedList<(string BookTitle, decimal? Average)>
                .ToPagedList(
                    list.Select(item => (item.BookTitle, item.Average)).AsQueryable(), 
                    query._parameters.PageNumber, 
                    query._parameters.PageSize);
            
            // Mapping (Mapster)
            return MapsterFunctions.MapListSourceToDestination
                <(string BookTitle, decimal? Average), GetDTO_AvgRatingBook>
                (pagingList);
        }
    }
}