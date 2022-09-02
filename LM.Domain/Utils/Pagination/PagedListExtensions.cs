using System.Linq;
using System.Threading.Tasks;

namespace LM.Domain.Utils.Pagination
{
    public static class PagedListExtensions
    {
        public static Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> superset, int pageNumber, int pageSize)
        {
            return Task.FromResult<IPagedList<T>>(new PagedList<T>(superset, pageNumber, pageSize));
        }
    }
}
