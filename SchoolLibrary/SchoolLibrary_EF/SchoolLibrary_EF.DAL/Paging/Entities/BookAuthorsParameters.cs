namespace SchoolLibrary_EF.DAL.Paging.Entities
{   
    public class BookAuthorsParameters : BaseParameters
    {
        public BookAuthorsParameters()
        {
            OrderBy = "BookId"; // default sorting
            Fields = "BookId, AuthorId, DateCreated, UpdateDate, DeleteDate"; // default fields (DataShaping)
        }
    }
}
