namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class UserParameters : BaseParameters
    {
        public string? UserName { get; set; } = default!;

        public UserParameters()
        {
            OrderBy = "Id"; // default sorting
            Fields = "Id, FirstName, LastName, DateCreated, DateUpdated, DateDeleted"; // default fields (DataShaping)
        }
    }
}
