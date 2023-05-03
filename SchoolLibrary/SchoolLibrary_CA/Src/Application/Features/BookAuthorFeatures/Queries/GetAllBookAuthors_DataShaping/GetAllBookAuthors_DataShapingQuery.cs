using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Pagging;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.BookAuthorFeatures.Queries.GetAllBookAuthors_DataShaping
{
    public class GetAllBookAuthors_DataShapingQuery : IRequest<PagedList<ShapedEntity>>
    {
        public BookAuthorParameter _parameters { get; }

        public GetAllBookAuthors_DataShapingQuery(BookAuthorParameter parameters)
        {
            _parameters = parameters;
        }
    }
}