﻿namespace Application.Features.UserIdentityFeatures.Queries.Common
{
    public class AddRoleModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}