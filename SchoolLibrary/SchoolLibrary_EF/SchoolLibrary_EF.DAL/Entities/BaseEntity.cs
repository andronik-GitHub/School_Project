namespace SchoolLibrary_EF.DAL.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}