using Application.Common.Pagging.Entities;
using Application.Features.BookDetailsFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookDetailsFeatures.Queries.GetAllBookDetails
{
    public class GetAllBookDetailsQuery : IRequest<IEnumerable<BookDetailsDTO>>
    {
        public BaseParameter _parameters { get; }

        public GetAllBookDetailsQuery(BaseParameter parameters)
        {
            _parameters = parameters;
        }
    }
}