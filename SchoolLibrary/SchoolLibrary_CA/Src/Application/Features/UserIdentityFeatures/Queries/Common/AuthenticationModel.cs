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
    }
}