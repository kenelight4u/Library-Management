using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Domain.Utils.Pagination
{
    [Serializable]
    public class PagedListMetaData : IPagedList
    {
        public PagedListMetaData()
        {

        }

        public PagedListMetaData(IPagedList pagedList)
        {
            PageCount = pagedList.PageCount;
            TotalItemCount = pagedList.TotalItemCount;
            PageNumber = pagedList.PageNumber;
            PageSize = pagedList.PageSize;
        }

        public int PageCount { get; protected set; }

        public int TotalItemCount { get; protected set; }

        public int PageNumber { get; protected set; }

        public int PageSize { get; protected set; }

        public bool HasPreviousPage { get; protected set; }

        public bool HasNextPage { get; protected set; }
    }
}
