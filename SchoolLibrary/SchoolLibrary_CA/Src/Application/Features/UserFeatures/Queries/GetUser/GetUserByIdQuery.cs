using Application.Features.UserFeatures.Queries.Common;
using MediatR;

namespace Application.Features.UserFeatures.Queries.GetUser
{
    public class GetUserByIdQuery : IRequest<UserDTO>
    {
        public Guid Id { get; set; } = default!;
    }
}