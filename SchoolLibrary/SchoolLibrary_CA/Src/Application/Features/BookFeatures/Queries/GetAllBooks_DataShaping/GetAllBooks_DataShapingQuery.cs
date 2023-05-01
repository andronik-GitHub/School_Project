using System.Dynamic;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.BookFeatures.Queries.GetAllBooks_DataShaping
{
    public class GetAllBooks_DataShapingQuery : IRequest<IEnumerable<ExpandoObject>>
    {
        public BookParameter _parameters { get; }

        public GetAllBooks_DataShapingQuery(BookParameter parameters)
        {
            _parameters = parameters;
        }
    }
}