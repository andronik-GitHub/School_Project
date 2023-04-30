namespace Application.Common.Pagging.Entities
{
    public class BookParameter : BaseParameter
    {
        public BookParameter()
        {
            OrderBy = "BookId"; // default sorting
        }
    }
}