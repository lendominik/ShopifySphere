using System.Drawing.Printing;

namespace Shop.Application.Common.PagedResult
{
    public class OrderPagedResult<T> : PagedResult<T>
    {
        public OrderPagedResult(IEnumerable<T> objects, int totalCount, int pageSize, int pageNumber) : base(objects, totalCount, pageSize, pageNumber)
        {
        }
    }
}
