using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Interfaces;
using Application.Common.Pagging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PublisherFeatures.Queries.GetAllPublishers_DataShaping
{
    public class GetAllPublishers_DataShapingQueryHandler 
        : IRequestHandler<GetAllPublishers_DataShapingQuery, PagedList<ShapedEntity>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<Publisher> _sortHelper;
        private readonly IDataShaper<Publisher> _dataShaper;
        
        public GetAllPublishers_DataShapingQueryHandler(
            ISchoolLibraryContext context,
            ISortHelper<Publisher> sortHelper,
            IDataShaper<Publisher> dataShaper)
        {
            _context = context;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }


        public async Task<PagedList<ShapedEntity>> Handle
            (GetAllPublishers_DataShapingQuery query, CancellationToken cancellationToken)
        {
            // Filtering
            var list = _context.Publishers
                .AsNoTracking()
                .AsQueryable();
            
            // Sorting
            list = _sortHelper.ApplySort(list, query._parameters.OrderBy);

            // DataShaping
            var shapedList = _dataShaper.ShapeData(list, query._parameters.Fields).AsQueryable();
            
            // Paging
            return await Task.Run(
                () => PagedList<ShapedEntity>.ToPagedList(
                    shapedList, 
                    query._parameters.PageNumber, 
                    query._parameters.PageSize),
                cancellationToken);
        }
    }
}