namespace Application.Features.GenreFeatures.Queries.Common
{
    public class GenreDTO
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; } = default!;
    }
}