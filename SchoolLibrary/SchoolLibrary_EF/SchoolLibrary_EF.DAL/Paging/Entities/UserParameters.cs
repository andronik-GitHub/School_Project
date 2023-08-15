namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class UserParameters : BaseParameters
    {
        public UserParameters()
        {
            OrderBy = "Id"; // default sorting
            Fields = "Id, UserName, Email, PasswordHash, PhoneNumber, FirstName, LastName, Country, City, Street," + 
                     " ConcurrencyStamp, SecurityStamp, DateCreated, DateUpdated, DateDeleted"; // default fields (DataShaping)
        }
    }
}
