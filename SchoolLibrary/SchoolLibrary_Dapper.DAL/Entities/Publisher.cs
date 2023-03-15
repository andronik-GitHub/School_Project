namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class Publisher
    {
        public Guid PublisherId { get; set; }
        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;
    }
}
