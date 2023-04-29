using MediatR;

namespace Application.Features.LoanFeatures.Commands.DeleteLoan
{
    public class DeleteLoanCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
    }
}