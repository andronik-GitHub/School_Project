using Application.Common.Pagging.Entities;
using Application.Features.AuthorFeatures.Queries.Common;
using MediatR;

namespace Application.Features.AuthorFeatures.Queries.GetAllAuthors
{
    public class GetAllAuthorsQuery : IRequest<IEnumerable<AuthorDTO>>
    {
        public AuthorParameter _parameters { get; }

        public GetAllAuthorsQuery(AuthorParameter parameters)
        {
            _parameters = parameters;
        }
    }
}