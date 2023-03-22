namespace SchoolLibrary_EF.BLL.DTO
{
    public class BookGenresDTO
    {
        //public Guid BookId { get; set; }
        //public Guid GenreId { get; set; }
        public string BookTitle { get; set; } = default!;
        public string GenreName { get; set; } = default!;


        public override string ToString()
        {
            return $"Book Title: {this.BookTitle}; Genre Name: {GenreName};";
        }
    }
}
