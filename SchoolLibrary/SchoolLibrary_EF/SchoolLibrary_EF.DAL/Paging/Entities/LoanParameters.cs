namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class LoanParameters : BaseParameters
    {
        public LoanParameters()
        {

            OrderBy = "LoanId"; // default sorting
        }
    }
}
