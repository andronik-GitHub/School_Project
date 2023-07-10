namespace SchoolLibrary_Dapper.BLL.DTOs.LoanDTOs
{
    public class UpdateDTO_Loan
    {
        public Guid LoanId { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}