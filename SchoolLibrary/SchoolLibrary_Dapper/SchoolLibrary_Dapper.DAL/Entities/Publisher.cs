namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class Publisher : BaseEntity
    {
        public Guid PublisherId
        {
            get => id;
            set => id = value;
        }
        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;
    }
}
