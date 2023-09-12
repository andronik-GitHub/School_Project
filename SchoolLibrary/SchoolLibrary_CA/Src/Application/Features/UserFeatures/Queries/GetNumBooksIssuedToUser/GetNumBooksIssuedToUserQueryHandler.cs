using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Common.Pagging;
using Application.Features.UserFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserFeatures.Queries.GetNumBooksIssuedToUser
{
    public class GetNumBooksIssuedToUserQueryHandler 
        : IRequestHandler<GetNumBooksIssuedToUserQuery, IEnumerable<GetDTO_NumBooksIssuedToUser>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<User> _sortHelper;

        public GetNumBooksIssuedToUserQueryHandler(ISchoolLibraryContext context, ISortHelper<User> sortHelper)
        {
            _context = context;
            _sortHelper = sortHelper;
        }
        
        
        /*  Count the number of books issued per user
         
            SELECT U.FirstName, U.LastName, COUNT(L.BookId) AS BooksLoaned 
            FROM Users U
            LEFT JOIN Loans L ON U.UserId = L.UserId
            GROUP BY U.UserId, U.FirstName, U.LastName;
            
         */
        public async Task<IEnumerable<GetDTO_NumBooksIssuedToUser>> Handle
            (GetNumBooksIssuedToUserQuery query, CancellationToken cancellationToken)
        {
            // Query
            var list = await _context.Users
                .AsNoTracking()
                .GroupJoin(
                    _context.Loans.AsNoTracking(),
                    u => u.UserId,
                    l => l.UserId,
                    (u, l) => new
                    {
                        FirstName = u.UserName.FirstName.Value,
                        LastName = u.UserName.LastName.Value,
                        BookLoaned = l.Count()
                    })
                .ToListAsync(cancellationToken: cancellationToken);
 
            // Paging
            var pagingList = PagedList<(string FirstName, string LastName, int BookLoaned)>
                .ToPagedList(
                    list.Select(item => (item.FirstName, item.LastName, item.BookLoaned)).AsQueryable(), 
                    query._parameters.PageNumber, 
                    query._parameters.PageSize);

            // Mapping (Mapster)
            return MapsterFunctions.MapListSourceToDestination
                <(string FirstName, string LastName, int BookLoaned), GetDTO_NumBooksIssuedToUser>
                (pagingList);
        }
    }
}