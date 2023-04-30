namespace Application.Common.Pagging.Entities
{
    public class PublisherParameter : BaseParameter
    {
        public PublisherParameter()
        {
            OrderBy = "PublisherId"; // default sorting
        }
    }
}