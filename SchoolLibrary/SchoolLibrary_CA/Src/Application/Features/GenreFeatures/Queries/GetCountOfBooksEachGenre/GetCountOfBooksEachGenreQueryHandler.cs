using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Common.Pagging;
using Application.Features.GenreFeatures.Queries.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GenreFeatures.Queries.GetCountOfBooksEachGenre
{
    public class GetCountOfBooksEachGenreQueryHandler 
        : IRequestHandler<GetCountOfBooksEachGenreQuery, IEnumerable<GetDTO_CountOfBooksEachGenre>>
    {
        private readonly ISchoolLibraryContext _context;

        public GetCountOfBooksEachGenreQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
        
        
        /*  Count the number of books of each genre
         
            SELECT G.Name AS GenreName, COUNT(BG.BookId) AS BookCount
            FROM Genres G
            LEFT JOIN BookGenres BG ON G.GenreId = BG.GenreId
            GROUP BY G.GenreId, G.Name;
            
         */
        public async Task<IEnumerable<GetDTO_CountOfBooksEachGenre>> Handle
            (GetCountOfBooksEachGenreQuery query, CancellationToken cancellationToken)
        {
            // Query
            var list = await _context.Genres
                .AsNoTracking()
                .GroupJoin(
                    _context.BookGenres.AsNoTracking(),
                    g => g.GenreId,
                    bg => bg.GenreId,
                    (g, bg) => new
                    {
                        GenreName = g.Name,
                        BookCount = bg.Count()
                    })
                .ToListAsync(cancellationToken: cancellationToken);
            
            // Paging
            var pagingList = PagedList<(string GenreName, int BookCount)>
                .ToPagedList(
                    list.Select(item => (item.GenreName, item.BookCount)).AsQueryable(), 
                    query._parameters.PageNumber, 
                    query._parameters.PageSize);
            
            // Mapping (Mapster)
            return MapsterFunctions.MapListSourceToDestination
                <(string GenreName, int BookCount), GetDTO_CountOfBooksEachGenre>
                (pagingList);
        }
    }
}