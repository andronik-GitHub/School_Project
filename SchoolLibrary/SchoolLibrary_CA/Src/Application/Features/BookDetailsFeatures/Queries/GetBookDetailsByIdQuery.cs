using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookDetailsFeatures.Queries
{
    public class GetBookDetailsByIdQuery : IRequest<BookDetails>
    {
        public Guid Id { get; set; }
        
        public class GetBookDetailsByIdQueryHandler : IRequestHandler<GetBookDetailsByIdQuery, BookDetails>
        {
            private readonly ISchoolLibraryContext _context;

            public GetBookDetailsByIdQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<BookDetails> Handle(GetBookDetailsByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _context.BookDetails
                    .AsNoTracking()
                    .Include(bd => bd.Book)
                    .FirstOrDefaultAsync(bd => bd.BookDetailId == query.Id, cancellationToken);
                
                return entity ?? throw new NotFoundException(nameof(BookDetails), query.Id);
            }
        }
    }
}