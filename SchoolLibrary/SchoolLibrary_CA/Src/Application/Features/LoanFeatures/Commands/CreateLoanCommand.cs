using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.LoanFeatures.Commands
{
    public class CreateLoanCommand : IRequest<Guid>
    {
        public DateTime LoanDate { get; set; } = default!;
        public DateTime ReturnDate { get; set;} = default!;
        public Guid UserId { get; set; } = default!;
        public Guid BookId { get; set; } = default!;
        
        public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;

            public CreateLoanCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }
            
            
            public async Task<Guid> Handle(CreateLoanCommand command, CancellationToken cancellationToken)
            {
                var entity = new Loan
                {
                    LoanId = Guid.NewGuid(),
                    LoanDate = command.LoanDate,
                    ReturnDate = command.ReturnDate,
                    UserId = command.UserId,
                    BookId = command.BookId
                };

                await _context.Loans.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.LoanId;
            }
        }
    }
}