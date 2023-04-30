namespace Application.Common.Pagging.Entities
{
    public class ReviewParameter : BaseParameter
    {
        public ReviewParameter()
        {
            OrderBy = "ReviewId"; // default sorting
        }
    }
}