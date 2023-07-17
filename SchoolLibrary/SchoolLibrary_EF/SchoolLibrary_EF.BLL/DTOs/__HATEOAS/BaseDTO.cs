namespace SchoolLibrary_EF.BLL.DTOs.__HATEOAS
{
    public abstract class BaseDTO
    {
        public Guid Id { get; set; }
        
        public IList<Link> Links = new List<Link>();
    }
}