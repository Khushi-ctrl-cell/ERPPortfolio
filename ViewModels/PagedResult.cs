using System;
using System.Collections.Generic;

namespace ERPPortfolio.ViewModels
{
    public class PagedResult<T>
    {
        public PagedResult(IList<T> items, int totalRecords, int pageNumber, int pageSize)
        {
            Items = items;
            TotalRecords = totalRecords;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        }

        public IList<T> Items { get; private set; }
        public int TotalRecords { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPreviousPage { get { return PageNumber > 1; } }
        public bool HasNextPage { get { return PageNumber < TotalPages; } }
    }
}
