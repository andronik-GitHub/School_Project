using SchoolLibrary_EF.BLL.DTO.HATEOAS;

namespace SchoolLibrary_EF.BLL.DTO
{
    public class LoanDTO : LinkBaseEntity
    {
        public Guid LoanId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set;}
        //public Guid UserId { get; set; }
        //public Guid BookId { get; set; }
        public string? UserFullName { get; set; } = default!;
        public string? BookTitle { get; set; } = default!;
        public string? BookISBN { get; set; } = default!;
    }
}
