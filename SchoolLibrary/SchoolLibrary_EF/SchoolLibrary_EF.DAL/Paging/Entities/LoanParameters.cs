namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class LoanParameters : BaseParameters
    {
        public LoanParameters()
        {
            OrderBy = "LoanId"; // default sorting
            Fields = "LoanId, LoanDate, ReturnDate, UserId, BookId, DateCreated, DateUpdated, DateDeleted"; // default fields (DataShaping)
        }
    }
}
