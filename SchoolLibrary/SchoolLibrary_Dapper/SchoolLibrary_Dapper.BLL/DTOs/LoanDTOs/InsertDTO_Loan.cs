namespace SchoolLibrary_Dapper.BLL.DTOs.LoanDTOs
{
    public class InsertDTO_Loan
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}