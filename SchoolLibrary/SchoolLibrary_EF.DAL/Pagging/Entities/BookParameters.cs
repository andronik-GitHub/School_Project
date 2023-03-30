namespace SchoolLibrary_EF.DAL.Pagging.Entities
{
    public class BookParameters : BaseParameters
    {
        public BookParameters()
        {
            OrderBy = "Title,PublishingYear desc"; // default sorting
        }
    }
}
