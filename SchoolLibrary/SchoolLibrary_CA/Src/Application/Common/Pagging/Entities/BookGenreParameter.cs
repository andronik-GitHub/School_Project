namespace Application.Common.Pagging.Entities
{
    public class BookGenreParameter : BaseParameter
    {
        public BookGenreParameter()
        {
            OrderBy = "BookId, GenreId"; // default sorting
        }
    }
}