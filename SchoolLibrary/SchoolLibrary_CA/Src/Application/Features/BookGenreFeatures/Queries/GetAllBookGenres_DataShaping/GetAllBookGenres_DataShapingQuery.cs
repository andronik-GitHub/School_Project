using System.Dynamic;
using Application.Common.Pagging;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.BookGenreFeatures.Queries.GetAllBookGenres_DataShaping
{
    public class GetAllBookGenres_DataShapingQuery : IRequest<PagedList<ExpandoObject>>
    {
        public BookGenreParameter _parameters { get; }

        public GetAllBookGenres_DataShapingQuery(BookGenreParameter parameters)
        {
            _parameters = parameters;
        }
    }
}