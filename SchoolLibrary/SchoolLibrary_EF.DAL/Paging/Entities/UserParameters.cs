namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class UserParameters : BaseParameters
    {
        public string? UserName { get; set; } = default!;

        public UserParameters()
        {
            OrderBy = "UserId"; // default sorting
        }
    }
}
