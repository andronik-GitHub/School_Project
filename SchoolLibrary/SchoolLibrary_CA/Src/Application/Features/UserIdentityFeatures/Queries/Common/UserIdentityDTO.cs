using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.UserIdentityFeatures.Queries.Common
{
    public class UserIdentityDTO
    {
        public DateTime DateCreated { get; set; }

        public Guid Id { get; set; } = default!;
        public string? UserName { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        
        
        public override string ToString()
        {
            return JsonSerializer // for visual display of values
                .Serialize(this, new JsonSerializerOptions
                {
                    WriteIndented = true, // spaces are included in json (relatively speaking, for beauty)
                    ReferenceHandler = ReferenceHandler.Preserve // "Preserve" to avoid circular references
                });
        }
    }
}