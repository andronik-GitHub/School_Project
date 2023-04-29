using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LoanFeatures.Commands.UpdateLoan
{
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

            entity = MapsterFunctions.MapSourceToDestination<UpdateLoanCommand, Loan>(command);

            _context.Loans.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.LoanId;
        }
    }
}