namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class GenreParameters : BaseParameters
    {
        public GenreParameters()
        {
            OrderBy = "GenreId"; // default sorting
            Fields = "GenreId, Name, DateCreated, DateUpdated, DateDeleted"; // default fields (DataShaping)
        }
    }
}
