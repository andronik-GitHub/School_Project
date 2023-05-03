using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.BookAuthorFeatures.Queries.GetBookAuthor_DataShaping
{
    public class GetBookAuthorByIds_DataShapingQuery : IRequest<ShapedEntity>
    {
        public Guid BookId { get; set; } = default!;
        public Guid AuthorId { get; set; } = default!;
        public BookAuthorParameter _parameters { get; }

        public GetBookAuthorByIds_DataShapingQuery(BookAuthorParameter parameters)
        {
            _parameters = parameters;
        }
    }
}