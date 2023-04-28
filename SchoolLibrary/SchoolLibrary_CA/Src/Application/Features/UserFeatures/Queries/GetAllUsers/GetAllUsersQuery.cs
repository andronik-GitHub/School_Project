using Application.Features.UserFeatures.Queries.Common;
using MediatR;

namespace Application.Features.UserFeatures.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDTO>>
    {
    }
}