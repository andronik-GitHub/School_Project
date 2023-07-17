namespace SchoolLibrary_EF.DAL.Paging.Entities
{
    public class PublisherParameters : BaseParameters
    {
        public PublisherParameters()
        {
            OrderBy = "PublisherId"; // default sorting
            Fields = "PublisherId, Name, Location, DateCreated, UpdateDate, DeleteDate"; // default fields (DataShaping)
        }
    }
}
