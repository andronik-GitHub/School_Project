using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.BookGenreFeatures.Queries.GetBookGenre_DataShaping
{
    public class GetBookGenreByIds_DataShapingQuery : IRequest<ShapedEntity>
    {
        public Guid BookId { get; set; } = default!;
        public Guid GenreId { get; set; } = default!;
        public BookGenreParameter _parameters { get; }

        public GetBookGenreByIds_DataShapingQuery(BookGenreParameter parameters)
        {
            _parameters = parameters;
        }
    }
}