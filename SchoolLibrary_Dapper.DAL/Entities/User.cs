namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Phone { get; set; } = default!;
    }
}
