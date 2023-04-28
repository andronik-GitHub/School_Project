using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserFeatures.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public Guid Id { get; set; } = default!;

        public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
        {
            private readonly ISchoolLibraryContext _context;
            public GetUserByIdQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }

            
            public async Task<User> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.UserId == query.Id, cancellationToken);
                
                return entity ?? throw new NotFoundException(nameof(User), query.Id);
            }
        }
    }
}