namespace Application.Common.Pagging.Entities
{
    public class BookDetailsParameter : BaseParameter
    {
        public BookDetailsParameter()
        {
            OrderBy = "BookDetailId"; // default sorting
        }
    }
}