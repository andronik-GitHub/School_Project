﻿using MediatR;

namespace Application.Features.UserIdentityFeatures.Commands.RegisterUserIdentity
{
    public class RegisterUserIdentityCommand : IRequest<Guid>
    {
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}