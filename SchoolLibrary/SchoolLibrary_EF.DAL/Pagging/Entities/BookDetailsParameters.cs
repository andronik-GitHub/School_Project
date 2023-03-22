namespace SchoolLibrary_EF.DAL.Pagging.Entities
{
    public class BookDetailsParameters : BaseParameters
    {
        public BookDetailsParameters()
        {
            OrderBy = "Pages";
        }
    }
}
