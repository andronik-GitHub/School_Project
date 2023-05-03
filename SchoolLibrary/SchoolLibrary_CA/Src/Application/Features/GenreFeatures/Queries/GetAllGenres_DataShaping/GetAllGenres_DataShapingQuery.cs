using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Pagging;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.GenreFeatures.Queries.GetAllGenres_DataShaping
{
    public class GetAllGenres_DataShapingQuery : IRequest<PagedList<ShapedEntity>>
    {
        public GenreParameter _parameters { get; }

        public GetAllGenres_DataShapingQuery(GenreParameter parameters)
        {
            _parameters = parameters;
        }
    }
}