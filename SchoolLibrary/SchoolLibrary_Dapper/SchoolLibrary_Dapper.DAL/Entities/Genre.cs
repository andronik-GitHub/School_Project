namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class Genre : BaseEntity
    {
        public Guid GenreId
        {
            get => id;
            set => id = value;
        }
        public string Name { get; set; } = default!;
    }
}
