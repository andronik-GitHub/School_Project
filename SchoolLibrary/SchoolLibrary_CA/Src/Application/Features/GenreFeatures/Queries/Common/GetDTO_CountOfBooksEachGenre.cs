namespace Application.Features.GenreFeatures.Queries.Common
{
    public class GetDTO_CountOfBooksEachGenre
    {
        public string GenreName { get; set; } = default!;
        public int BookCount { get; set; } = default!;
    }
}