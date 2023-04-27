using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserFeatures.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<User>>
    {
        public class GetAllUsersQueryHangler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
        {
            private readonly ISchoolLibraryContext _context;

            public GetAllUsersQueryHangler(ISchoolLibraryContext context)
            {
                _context = context;
            }
            
            public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
            {
                var list = await _context.Users.AsNoTracking().ToListAsync(cancellationToken);

                return list.AsReadOnly();
            }
        }
    }
}