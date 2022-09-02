using LM.Domain.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Domain.Common.ViewModel
{
    // <summary>
    /// Class SearchVM.
    /// </summary>
    public class SearchVM
    {
        /// <summary>
        /// Gets or sets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        [Range(1, int.MaxValue, ErrorMessage = "PageIndex must be greater than 0")]
        public int PageIndex { get; set; } = CoreConstants.PaginationConsts.PageIndex;

        /// <summary>
        /// Gets or sets the page total.
        /// </summary>
        /// <value>The page total.</value>
        public int? PageTotal { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        [Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than 0")]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Gets the page skip.
        /// </summary>
        /// <value>The page skip.</value>
        public int PageSkip => (PageIndex - 1) * PageSize;
        /// <summary>
        /// Gets or sets the filters.
        /// </summary>
        /// <value>The filters.</value>
        public Filter Filter { get; set; } 
    }

    /// <summary>
    /// Class Filter.
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>The keyword.</value>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets the filter column.
        /// </summary>
        /// <value>The filter column.</value>
        public string FilterColumn { get; set; }

    }
}
