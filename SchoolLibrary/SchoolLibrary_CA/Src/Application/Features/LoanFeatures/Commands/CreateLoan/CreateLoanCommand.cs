using MediatR;

namespace Application.Features.LoanFeatures.Commands.CreateLoan
{
    public class CreateLoanCommand : IRequest<Guid>
    {
        public DateTime LoanDate { get; set; } = default!;
        public DateTime ReturnDate { get; set;} = default!;
        public Guid UserId { get; set; } = default!;
        public Guid BookId { get; set; } = default!;
    }
}