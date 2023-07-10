namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class Book : BaseEntity
    {
        public Guid BookId
        {
            get => id;
            set => id = value;
        }
        public string Title { get; set; } = default!;
        public int PublishingYear { get; set; }
        public Guid PublisherId { get; set; }
        public string ISBN { get; set; } = default!;
    }
}
