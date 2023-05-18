﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.UserIdentityFeatures.Queries.Common;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Queries.GetUserIdentity
{
    public class GetUserIdentityQueryHandler : IRequestHandler<GetUserIdentityQuery, UserIdentityDTO>
    {
        private readonly IUserManager _userManager;
        
        public GetUserIdentityQueryHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }


        public async Task<UserIdentityDTO> Handle(GetUserIdentityQuery query, CancellationToken cancellationToken)
        {
            var entity = await _userManager.GetUserByIdAsync(query.Id);
            if (entity is not UserIdentityDTO userDTO) 
                throw new BadRequestException("The received data does not match the requested data type!");
            return userDTO ?? throw new NotFoundException(nameof(UserIdentityDTO), query.Id);
        }
    }
}