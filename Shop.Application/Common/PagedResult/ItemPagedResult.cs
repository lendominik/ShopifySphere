namespace Shop.Application.Common.PagedResult
{
    public class ItemPagedResult<T> : PagedResult<T>
    {
        public IEnumerable<Domain.Entities.Category> Categories { get; set; }
        public ItemPagedResult(IEnumerable<T> objects, int totalCount, int pageSize, int pageNumber, IEnumerable<Domain.Entities.Category> categories) : base(objects, totalCount, pageSize, pageNumber)
        {
            Objects = objects;
            PageSize = pageSize;
            TotalItemsCount = totalCount;
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = Math.Min(ItemsFrom + pageSize - 1, TotalItemsCount);
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            Categories = categories;
        }
    }
}
