namespace Application.Common.Pagging.Entities
{
    public class BookAuthorParameter : BaseParameter
    {
        public BookAuthorParameter()
        {
            OrderBy = "BookId, AuthorId"; // default sorting
        }
    }
}