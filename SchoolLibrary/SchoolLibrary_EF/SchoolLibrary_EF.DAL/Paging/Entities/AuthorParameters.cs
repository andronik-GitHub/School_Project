namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class AuthorParameters : BaseParameters
    {
        public AuthorParameters()
        {
            OrderBy = "AuthorId"; // default sorting
            Fields = "AuthorId, FirstName, LastName, DateCreated, UpdateDate, DeleteDate"; // default fields (DataShaping)
        }
    }
}
