namespace Application.Common.Pagging.Entities
{
    public class GenreParameter : BaseParameter
    {
        public GenreParameter()
        {
            OrderBy = "GenreId"; // default sorting
        }
    }
}