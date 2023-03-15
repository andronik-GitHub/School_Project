namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = default!;
    }
}
