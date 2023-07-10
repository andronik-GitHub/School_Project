namespace SchoolLibrary_Dapper.BLL.DTOs.AuthorDTOs
{
    public class GetDTO_Author
    {
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; } = default!;
        public DateTime Birthdate { get; set; }
    }
}
