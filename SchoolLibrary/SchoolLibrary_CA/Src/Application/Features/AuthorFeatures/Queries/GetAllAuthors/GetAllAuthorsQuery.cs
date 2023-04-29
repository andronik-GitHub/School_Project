using Application.Features.AuthorFeatures.Queries.Common;
using MediatR;

namespace Application.Features.AuthorFeatures.Queries.GetAllAuthors
{
    public class GetAllAuthorsQuery : IRequest<IEnumerable<AuthorDTO>>
    {
    }
}