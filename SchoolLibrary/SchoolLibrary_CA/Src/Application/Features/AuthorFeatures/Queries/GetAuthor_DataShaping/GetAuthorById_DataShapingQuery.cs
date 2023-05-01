using System.Dynamic;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.AuthorFeatures.Queries.GetAuthor_DataShaping
{
    public class GetAuthorById_DataShapingQuery : IRequest<ExpandoObject>
    {
        public Guid Id { get; set; }
        public AuthorParameter _parameters { get; }

        public GetAuthorById_DataShapingQuery(AuthorParameter parameters)
        {
            _parameters = parameters;
        }
    }
}