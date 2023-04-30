using Application.Common.Pagging.Entities;
using Application.Features.AuthorFeatures.Queries.Common;
using MediatR;

namespace Application.Features.AuthorFeatures.Queries.GetAllAuthors
{
    public class GetAllAuthorsQuery : IRequest<IEnumerable<AuthorDTO>>
    {
        public BaseParameter _parameters { get; }

        public GetAllAuthorsQuery(BaseParameter parameters)
        {
            _parameters = parameters;
        }
    }
}