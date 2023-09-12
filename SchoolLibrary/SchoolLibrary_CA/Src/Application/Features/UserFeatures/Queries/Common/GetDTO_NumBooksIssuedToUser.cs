namespace Application.Features.UserFeatures.Queries.Common
{
    public class GetDTO_NumBooksIssuedToUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int BooksLoaned { get; set; } = default!;
    }
}