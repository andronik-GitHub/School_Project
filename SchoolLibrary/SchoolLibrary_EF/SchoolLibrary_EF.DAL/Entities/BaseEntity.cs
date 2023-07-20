using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SchoolLibrary_EF.DAL.Entities
{
    public class BaseEntity
    {
        [NotMapped]
        [JsonIgnore]
        public Guid Id { get; set; }
        
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}