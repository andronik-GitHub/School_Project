using Application.Common.Pagging.Entities;
using Application.Features.GenreFeatures.Queries.Common;
using MediatR;

namespace Application.Features.GenreFeatures.Queries.GetAllGenres
{
    public class GetAllGenresQuery : IRequest<IEnumerable<GenreDTO>>
    {
        public BaseParameter _parameters { get; }

        public GetAllGenresQuery(BaseParameter parameters)
        {
            _parameters = parameters;
        }
    }
}