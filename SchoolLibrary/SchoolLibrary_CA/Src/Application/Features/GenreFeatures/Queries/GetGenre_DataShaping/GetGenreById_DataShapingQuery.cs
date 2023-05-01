using System.Dynamic;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.GenreFeatures.Queries.GetGenre_DataShaping
{
    public class GetGenreById_DataShapingQuery : IRequest<ExpandoObject>
    {
        public Guid Id { get; set; } = default!;
        public GenreParameter _parameters { get; }

        public GetGenreById_DataShapingQuery(GenreParameter parameters)
        {
            _parameters = parameters;
        }
    }
}