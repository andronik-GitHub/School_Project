namespace SchoolLibrary_EF.DAL.Paging.Entities
{   
    public class BookAuthorsParameters : BaseParameters
    {
        public BookAuthorsParameters()
        {
            OrderBy = "BookId"; // default sorting
        }
    }
}
