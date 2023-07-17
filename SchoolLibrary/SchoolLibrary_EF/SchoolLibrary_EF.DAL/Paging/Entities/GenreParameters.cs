namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class GenreParameters : BaseParameters
    {
        public GenreParameters()
        {
            OrderBy = "GenreId"; // default sorting
            Fields = "GenreId, Name, DateCreated, UpdateDate, DeleteDate"; // default fields (DataShaping)
        }
    }
}
