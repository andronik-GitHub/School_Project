namespace SchoolLibrary_EF.BLL.DTO.HATEOAS
{
    public abstract class LinkBaseEntity
    {
        public IList<Link> Links = new List<Link>();
    }
}