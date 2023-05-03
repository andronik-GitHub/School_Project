using Application.Common.HATEOS;

namespace Application.Features.LoanFeatures.Queries.Common
{
    public class LoanDTO: LinkBaseEntity
    {
        public Guid LoanId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set;}
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}