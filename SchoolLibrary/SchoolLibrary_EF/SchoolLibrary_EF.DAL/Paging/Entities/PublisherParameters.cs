namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class PublisherParameters : BaseParameters
    {
        public PublisherParameters()
        {
            OrderBy = "PublisherId"; // default sorting
        }
    }
}
