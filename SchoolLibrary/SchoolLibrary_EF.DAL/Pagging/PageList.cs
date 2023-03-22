﻿namespace SchoolLibrary_EF.DAL.Pagging
{
    public class PageList<TEntity> : List<TEntity>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PageList(List<TEntity> entities, int count, int pageNumber, int pageSize)
        {
            TotalPages = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(entities);
        }

        public static PageList<TEntity> ToPagedList(IQueryable<TEntity> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PageList<TEntity>(items, count, pageNumber, pageSize);
        }
    }
}
