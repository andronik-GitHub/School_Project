using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;

namespace SchoolLibrary_EF.BLL.DTOs.PublisherDTOs
{
    public class GetDTO_Publisher : BaseDTO
    {
        public Guid PublisherId { get; set; }
        public string Name { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
    }
}
