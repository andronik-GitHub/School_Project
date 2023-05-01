using System.Dynamic;
using Application.Common.Interfaces;
using Application.Common.Pagging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserFeatures.Queries.GetAllUsers_DataShaping
{
    public class GetAllUsers_DataShapingQueryHandler 
        : IRequestHandler<GetAllUsers_DataShapingQuery, PagedList<ExpandoObject>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<User> _sortHelper;
        private readonly IDataShaper<User> _dataShaper;
        
        public GetAllUsers_DataShapingQueryHandler(
            ISchoolLibraryContext context,
            ISortHelper<User> sortHelper,
            IDataShaper<User> dataShaper)
        {
            _context = context;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }


        public async Task<PagedList<ExpandoObject>> Handle
            (GetAllUsers_DataShapingQuery query, CancellationToken cancellationToken)
        {
            // Filtering
            var list = _context.Users
                .AsNoTracking()
                .Include(r => r.UserName)
                .Include(r => r.Address)
                .AsQueryable();
            
            // Sorting
            list = _sortHelper.ApplySort(list, query._parameters.OrderBy);

            // DataShaping
            var shapedList = _dataShaper.ShapeData(list, query._parameters.Fields).AsQueryable();
            
            // Paging
            return await Task.Run(
                () => PagedList<ExpandoObject>.ToPagedList(
                    shapedList, 
                    query._parameters.PageNumber, 
                    query._parameters.PageSize),
                cancellationToken);
        }
    }
}