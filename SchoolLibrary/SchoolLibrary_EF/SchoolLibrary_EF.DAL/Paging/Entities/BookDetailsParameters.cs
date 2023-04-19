namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class BookDetailsParameters : BaseParameters
    {
        public BookDetailsParameters()
        {
            OrderBy = "Pages"; // default sorting
        }
    }
}
