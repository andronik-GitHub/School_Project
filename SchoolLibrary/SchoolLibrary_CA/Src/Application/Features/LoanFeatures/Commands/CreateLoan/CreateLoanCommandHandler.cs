using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;

namespace Application.Features.LoanFeatures.Commands.CreateLoan
{
    public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;

        public CreateLoanCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
            
            
        public async Task<Guid> Handle(CreateLoanCommand command, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<CreateLoanCommand, Loan>(command);

            await _context.Loans.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.LoanId;
        }
    }
}