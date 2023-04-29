using Application.Features.BookAuthorFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookAuthorFeatures.Queries.GetAllBookAuthors
{
    public class GetAllBookAuthorsQuery : IRequest<IEnumerable<BookAuthorsDTO>>
    {
    }
}