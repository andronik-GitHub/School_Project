using System.Dynamic;
using Application.Common.Interfaces;
using Application.Common.Pagging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthorFeatures.Queries.GetAllAuthors_DataShaping
{
    public class GetAllAuthors_DataShapingQueryHandler 
        : IRequestHandler<GetAllAuthors_DataShapingQuery, PagedList<ExpandoObject>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<Author> _sortHelper;
        private readonly IDataShaper<Author> _dataShaper;

        public GetAllAuthors_DataShapingQueryHandler(
            ISchoolLibraryContext context,
            ISortHelper<Author> sortHelper,
            IDataShaper<Author> dataShaper)
        {
            _context = context;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }
        
        
        public async Task<PagedList<ExpandoObject>> Handle
            (GetAllAuthors_DataShapingQuery query, CancellationToken cancellationToken)
        {
            // Filtering
            var list = _context.Authors.AsNoTracking().Where(a => 
                a.Birthdate.Year >= query._parameters.MinYearOfBirth && 
                a.Birthdate.Year <= query._parameters.MaxYearOfBirth);
            
            // Searching
            SearchByName(ref list, query._parameters.FullName);
            
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

        private static void SearchByName(ref IQueryable<Author> entities, string? Name)
        {
            if (!entities.Any() || string.IsNullOrWhiteSpace(Name)) return;

            entities = entities
                .Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(Name.Trim().ToLower()));
        }
    }
}