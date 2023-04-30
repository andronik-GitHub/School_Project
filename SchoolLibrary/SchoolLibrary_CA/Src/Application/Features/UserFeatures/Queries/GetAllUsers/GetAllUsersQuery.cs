using Application.Common.Pagging.Entities;
using Application.Features.UserFeatures.Queries.Common;
using MediatR;

namespace Application.Features.UserFeatures.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDTO>>
    {
        public BaseParameter _parameters { get; }

        public GetAllUsersQuery(BaseParameter parameters)
        {
            _parameters = parameters;
        }
    }
}