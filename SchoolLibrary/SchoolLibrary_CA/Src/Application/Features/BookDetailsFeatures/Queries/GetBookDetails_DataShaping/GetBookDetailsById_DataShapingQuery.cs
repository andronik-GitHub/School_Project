using System.Dynamic;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.BookDetailsFeatures.Queries.GetBookDetails_DataShaping
{
    public class GetBookDetailsById_DataShapingQuery : IRequest<ExpandoObject>
    {
        public Guid Id { get; set; } = default!;
        public BookDetailsParameter _parameters { get; }

        public GetBookDetailsById_DataShapingQuery(BookDetailsParameter parameters)
        {
            _parameters = parameters;
        }
    }
}