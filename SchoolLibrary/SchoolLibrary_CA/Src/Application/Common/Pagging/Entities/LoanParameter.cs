namespace Application.Common.Pagging.Entities
{
    public class LoanParameter : BaseParameter
    {
        public LoanParameter()
        {
            OrderBy = "LoanId"; // default sorting
        }
    }
}