using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LoanFeatures.Commands
{
    public class DeleteLoanCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        
        public class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;

            public DeleteLoanCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<Guid> Handle(DeleteLoanCommand command, CancellationToken cancellationToken)
            {
                var entity = await _context.Loans
                    .AsNoTracking()
                    .FirstOrDefaultAsync(l => l.LoanId == command.Id, cancellationToken);

                if (entity == null) throw new NotFoundException(nameof(Loan), command.Id);
                
                _context.Loans.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.LoanId;
            }
        }
    }
}