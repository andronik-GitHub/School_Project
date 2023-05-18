using Application.Features.UserIdentityFeatures.Queries.Common;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Queries.GetUsersIdentity
{
    public class GetUsersIdentityQuery : IRequest<IEnumerable<UserIdentityDTO>>
    {
        
    }
}