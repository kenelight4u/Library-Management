using LM.Application.Interfaces.Persistence;
using LM.Domain.Common;
using LM.Domain.Common.ViewModel;
using LM.Domain.Entities;
using LM.Domain.Utils;
using LM.Domain.Utils.Pagination;
using LM.DTOs.Response.BookGenresVM;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LM.Services.Implementations.Features.BookGenreFeatures.Queries
{
    public class GetAllAllBooksGenresQueryHandler : IRequestHandler<SearchVM, ResultModel<PagedList<BookGenresVM>>>
    {
        private readonly IStoreManager<BookGenres> _bookGenre;

        public GetAllAllBooksGenresQueryHandler(IStoreManager<BookGenres> bookGenre)
        {
            _bookGenre = bookGenre;
        }

        public async Task<ResultModel<PagedList<BookGenresVM>>> Handle(SearchVM request, CancellationToken cancellationToken)
        {
            var query = _bookGenre.DataStore.GetAllQuery();

            return await SearchBooksGenre(query, request);
        }

        private async Task<ResultModel<PagedList<BookGenresVM>>> SearchBooksGenre(IQueryable<BookGenres> query, SearchVM model)
        {
            if (model.Filter is not null)
                query = BuildQueryFilter(query, model.Filter);

            var bookGenres = await query.OrderByDescending(x => x.CreationTime)
                .ToPagedListAsync(model.PageIndex, model.PageSize);

            var bookGenresVms = bookGenres.Select(x => (BookGenresVM)x).ToList();

            var data = new PagedList<BookGenresVM>(bookGenresVms, model.PageIndex, model.PageSize, bookGenres.TotalItemCount);

            return new ResultModel<PagedList<BookGenresVM>> { Data = data, Message = $"Found {bookGenres.Count} Genres." };
        }

        private IQueryable<BookGenres> BuildQueryFilter(IQueryable<BookGenres> query, Filter filter)
        {

            switch (filter.FilterColumn)
            {
                case "Name":
                    query = query.Where(x => x.Name.ToLower().Contains(filter.Keyword.ToLower()));
                    break;
                default:
                    break;
            }

            return query;
        }

           
    }
    

    // <summary>
    /// Class SearchVM.
    /// </summary>
    public class SearchVM : IRequest<ResultModel<PagedList<BookGenresVM>>>
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
