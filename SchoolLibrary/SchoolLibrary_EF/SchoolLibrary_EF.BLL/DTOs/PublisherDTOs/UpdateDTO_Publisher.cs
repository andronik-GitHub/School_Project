namespace SchoolLibrary_EF.BLL.DTOs.PublisherDTOs
{
    public class UpdateDTO_Publisher
    {
        public Guid PublisherId { get; set; }
        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;
    }
}