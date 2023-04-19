using SchoolLibrary_EF.BLL.DTO.HATEOAS;

namespace SchoolLibrary_EF.BLL.DTO
{
    public class PublisherDTO : LinkBaseEntity
    {
        public Guid PublisherId { get; set; }
        public string? Name { get; set; } = default!;
        //public string Location { get; set; } = default!;
        public string? Country { get; set; } = default!;
        public string? City { get; set; } = default!;
        public string? Street { get; set; } = default!;
    }
}
