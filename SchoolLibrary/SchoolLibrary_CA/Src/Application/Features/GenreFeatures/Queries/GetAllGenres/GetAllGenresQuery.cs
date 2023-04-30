using Application.Common.Pagging.Entities;
using Application.Features.GenreFeatures.Queries.Common;
using MediatR;

namespace Application.Features.GenreFeatures.Queries.GetAllGenres
{
    public class GetAllGenresQuery : IRequest<IEnumerable<GenreDTO>>
    {
        public GenreParameter _parameters { get; }

        public GetAllGenresQuery(GenreParameter parameters)
        {
            _parameters = parameters;
        }
    }
}