namespace SchoolLibrary_Dapper.BLL.DTOs.BookDetailsDTOs
{
    public class GetDTO_BookDetails
    {
        public Guid BookDetailId { get; set; }
        public Guid BookId { get; set; }
        public int Pages { get; set; }
    }
}
