namespace Application.Features.BookFeatures.Queries.Common
{
    public class GetDTO_AvgRatingBook
    {
        public string BookTitle { get; set; } = default!;
        public decimal? Average { get; set; } = default!;
    }
}