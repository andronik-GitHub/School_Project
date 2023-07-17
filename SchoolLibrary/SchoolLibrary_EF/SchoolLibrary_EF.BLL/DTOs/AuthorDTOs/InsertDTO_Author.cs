namespace SchoolLibrary_EF.BLL.DTOs.AuthorDTOs
{
    public class InsertDTO_Author
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Birthdate { get; set; }
        public string Nationality { get; set; } = default!;
    }
}