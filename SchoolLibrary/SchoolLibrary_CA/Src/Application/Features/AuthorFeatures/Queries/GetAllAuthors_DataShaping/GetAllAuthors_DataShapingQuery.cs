using System.Dynamic;
using Application.Common.Pagging;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.AuthorFeatures.Queries.GetAllAuthors_DataShaping
{
    public class GetAllAuthors_DataShapingQuery : IRequest<PagedList<ExpandoObject>>
    {
        public AuthorParameter _parameters { get; }

        public GetAllAuthors_DataShapingQuery(AuthorParameter parameters)
        {
            _parameters = parameters;
        }
    }
}