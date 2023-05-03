using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.BookFeatures.Queries.GetBook_DataShaping
{
    public class GetBookById_DataShapingQuery : IRequest<ShapedEntity>
    {
        public Guid Id { get; set; } = default!;
        public BookParameter _parameters { get; }

        public GetBookById_DataShapingQuery(BookParameter parameters)
        {
            _parameters = parameters;
        }
    }
}