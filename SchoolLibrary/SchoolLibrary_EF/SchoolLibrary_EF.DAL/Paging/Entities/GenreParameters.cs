namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class GenreParameters : BaseParameters
    {
        public GenreParameters()
        {

            OrderBy = "GenreId"; // default sorting
        }
    }
}
