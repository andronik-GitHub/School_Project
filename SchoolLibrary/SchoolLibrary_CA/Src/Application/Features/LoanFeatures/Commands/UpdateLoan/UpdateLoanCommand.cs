using MediatR;

namespace Application.Features.LoanFeatures.Commands.UpdateLoan
{
    public class UpdateLoanCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        public DateTime LoanDate { get; set; } = default!;
        public DateTime ReturnDate { get; set;} = default!;
        public Guid UserId { get; set; } = default!;
        public Guid BookId { get; set; } = default!;
    }
}