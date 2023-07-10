namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class Review : BaseEntity
    {
        public Guid ReviewId
        {
            get => id;
            set => id = value;
        }
        public Guid UserId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = default!;
    }
}
