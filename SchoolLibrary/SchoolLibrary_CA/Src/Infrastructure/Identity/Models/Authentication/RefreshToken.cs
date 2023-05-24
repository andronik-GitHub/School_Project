namespace Infrastructure.Identity.Models.Authentication
{
    public class RefreshToken
    {
        public string Token { get; set; } = default!;
        public DateTime Expires  { get; set; } = default!;
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime Created  { get; set; } = default!;
        public DateTime? Revoked { get; set; } = default!;
        public bool IsActive => Revoked == null && !IsExpired;
    }
}