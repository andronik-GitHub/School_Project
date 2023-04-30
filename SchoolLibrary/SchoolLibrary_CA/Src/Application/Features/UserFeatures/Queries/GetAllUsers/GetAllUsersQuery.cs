using Application.Common.Pagging.Entities;
using Application.Features.UserFeatures.Queries.Common;
using MediatR;

namespace Application.Features.UserFeatures.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDTO>>
    {
        public UserParameter _parameters { get; }

        public GetAllUsersQuery(UserParameter parameters)
        {
            _parameters = parameters;
        }
    }
}