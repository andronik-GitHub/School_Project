namespace Application.Features.UserIdentityFeatures.Queries.Common
{
    public class TokenRequestModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}