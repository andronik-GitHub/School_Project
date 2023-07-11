namespace SchoolLibrary_EF.DAL.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        
        public DateTime DateCreated { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}