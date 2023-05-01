using System.Dynamic;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.UserFeatures.Queries.GetUser_DataShaping
{
    public class GetUserById_DataShapingQuery : IRequest<ExpandoObject>
    {
        public Guid Id { get; set; } = default!;
        public UserParameter _parameters { get; }

        public GetUserById_DataShapingQuery(UserParameter parameters)
        {
            _parameters = parameters;
        }
    }
}