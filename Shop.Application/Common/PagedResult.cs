using Shop.Application.Order;

namespace Shop.Application.Common
{
    public class PagedResult<T>
    {
        public int TotalItemsCount { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<T> Objects { get; set; }

        public PagedResult(IEnumerable<T> objects, int totalCount, int pageSize, int pageNumber)
        {
            Objects = objects;
            PageSize = pageSize;
            TotalItemsCount = totalCount;
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = Math.Min(ItemsFrom + pageSize - 1, TotalItemsCount);
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
