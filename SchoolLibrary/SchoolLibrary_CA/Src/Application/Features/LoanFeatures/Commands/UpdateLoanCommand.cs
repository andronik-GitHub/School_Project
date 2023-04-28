using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LoanFeatures.Commands
{
    public class UpdateLoanCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        public DateTime LoanDate { get; set; } = default!;
        public DateTime ReturnDate { get; set;} = default!;
        public Guid UserId { get; set; } = default!;
        public Guid BookId { get; set; } = default!;
        
        public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;

            public UpdateLoanCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<Guid> Handle(UpdateLoanCommand command, CancellationToken cancellationToken)
            {
                var entity = await _context.Loans
                    .AsNoTracking()
                    .FirstOrDefaultAsync(l => l.LoanId == command.Id, cancellationToken: cancellationToken);
                
                if (entity == null) throw new NotFoundException(nameof(Loan), command.Id);

                entity.LoanDate = command.LoanDate;
                entity.ReturnDate = command.ReturnDate;
                entity.UserId = command.UserId;
                entity.BookId = command.BookId;

                await _context.SaveChangesAsync(cancellationToken);
                return entity.LoanId;
            }
        }
    }
}