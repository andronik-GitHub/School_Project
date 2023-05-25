using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Features.UserIdentityFeatures.Queries.Common
{
    public class AuthenticationModel
    {
        public string Message { get; set; } = default!;
        public bool IsAuthenticated { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public List<string> Roles { get; set; } = default!;
        public string Token { get; set; } = default!;

        [JsonIgnore]
        public string RefreshToken { get; set; } = default!;
        public DateTime RefreshTokenExpiration { get; set; } = default!;
        
        public override string ToString()
        {
            return JsonSerializer // for visual display of values
                .Serialize(this, new JsonSerializerOptions
                {
                    WriteIndented = true, // spaces are included in json (relatively speaking, for beauty)
                });
        }
    }
}