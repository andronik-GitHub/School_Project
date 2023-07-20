namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class BookGenresParameters : BaseParameters
    {
        public BookGenresParameters()
        {
            OrderBy = "BookId"; // default sorting
            Fields = "BookId, GenreId, DateCreated, DateUpdated, DateDeleted"; // default fields (DataShaping)
        }
    }
}
