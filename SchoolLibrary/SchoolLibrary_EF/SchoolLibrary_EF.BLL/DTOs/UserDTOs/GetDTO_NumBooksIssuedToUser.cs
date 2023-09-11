namespace SchoolLibrary_EF.BLL.DTOs.UserDTOs
{
    public class GetDTO_NumBooksIssuedToUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int BooksLoaned { get; set; } = default!;
    }
}