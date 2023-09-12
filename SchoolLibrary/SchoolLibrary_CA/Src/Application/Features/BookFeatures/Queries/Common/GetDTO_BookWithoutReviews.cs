namespace Application.Features.BookFeatures.Queries.Common
{
    public class GetDTO_BookWithoutReviews
    {
        public Guid BookId { get; set; } = default!;
        public string BookTitle { get; set; } = default!;
    }
}