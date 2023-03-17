namespace SchoolLibrary_EF.BLL.DTO
{
    public class AuthorDTO
    {
        public Guid Authorid { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Birthdate { get; set; }
        //public string Nationality { get; set; } = default!;
        public string Country { get; set; } = default!;
    }
}
