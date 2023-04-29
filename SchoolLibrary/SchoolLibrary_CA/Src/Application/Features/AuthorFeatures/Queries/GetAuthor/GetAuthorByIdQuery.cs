using Application.Features.AuthorFeatures.Queries.Common;
using MediatR;

namespace Application.Features.AuthorFeatures.Queries.GetAuthor
{
    public class GetAuthorByIdQuery : IRequest<AuthorDTO>
    {
        public Guid Id { get; set; }
    }
}