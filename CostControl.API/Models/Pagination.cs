using System;

namespace CostControl.API.Models
{
    public class Pagination/*<TEntity> : IEntity*/
    {
        private int MaxPageSize { get; set; } = 500;

        public int PageNumber { get; set; } = 1; //Current page number, page index

        private int pageSize { get; set; } = 10;

        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }

        public int TotalPages { get; private set; } = 0;

        public string SortOrder { get; set; } = "Id";

        public string SortDirection { get; set; } = "ASC";

        public string CurrentSortOrder { get; set; } = string.Empty;

        public string SearchKey { get; set; } = string.Empty;

        public Pagination() { }

        public Pagination(int rowCount, int pageIndex, int pageSize)
        {
            PageNumber = pageIndex;
            RowCount = rowCount;
            TotalPages = (int)Math.Ceiling(rowCount / (double)pageSize);
        }

        public int RowCount { get; set; } = 0;

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;
    }
}