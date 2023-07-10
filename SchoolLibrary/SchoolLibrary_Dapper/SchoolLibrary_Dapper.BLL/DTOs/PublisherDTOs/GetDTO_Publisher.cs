namespace SchoolLibrary_Dapper.BLL.DTOs.PublisherDTOs
{
    public class GetDTO_Publisher
    {
        public Guid PublisherId { get; set; }
        public string Name { get; set; } = default!;
    }
}
