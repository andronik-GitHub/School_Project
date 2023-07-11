using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace SchoolLibrary_Dapper.DAL.Entities
{
    public abstract class BaseEntity
    {
        [IgnoreDataMember]
        [NotMapped]
        public Guid id { get; set; }
    
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}