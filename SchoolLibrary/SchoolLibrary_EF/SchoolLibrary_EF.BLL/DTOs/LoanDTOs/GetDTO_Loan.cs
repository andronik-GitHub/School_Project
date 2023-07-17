using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;

namespace SchoolLibrary_EF.BLL.DTOs.LoanDTOs
{
    public class GetDTO_Loan : BaseDTO
    {
        public Guid LoanId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set;}
        public string? UserFullName { get; set; } = default!;
        public string? BookTitle { get; set; } = default!;
        public string? BookISBN { get; set; } = default!;
    }
}
