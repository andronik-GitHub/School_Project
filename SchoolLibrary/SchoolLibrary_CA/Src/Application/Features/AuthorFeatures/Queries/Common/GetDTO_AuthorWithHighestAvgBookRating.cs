namespace Application.Features.AuthorFeatures.Queries.Common
{
    public class GetDTO_AuthorWithHighestAvgBookRating
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public decimal AverageRating { get; set; } = default!;
    }
}