namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class AuthorParameters : BaseParameters
    {
        public uint MinYearOfBirth { get; set; }
        public uint MaxYearOfBirth { get; set; } = (uint)DateTime.Now.Year;

        public bool ValidYearRand => MaxYearOfBirth > MinYearOfBirth;

        public AuthorParameters()
        {
            OrderBy = "AuthorId"; // default sorting
            Fields = "AuthorId, FirstName, LastName, DateCreated, UpdateDate, DeleteDate"; // default fields (DataShaping)
        }
    }
}
