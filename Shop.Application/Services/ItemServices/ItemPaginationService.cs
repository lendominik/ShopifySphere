namespace Shop.Application.Services.ItemServices
{
    public interface IItemPaginationService
    {
        IEnumerable<Domain.Entities.Item> PaginationSkipAndTake(IQueryable<Domain.Entities.Item> items, int pageNumber, int pageSize);
    }

    public class ItemPaginationService : IItemPaginationService
    {
        public IEnumerable<Domain.Entities.Item> PaginationSkipAndTake(IQueryable<Domain.Entities.Item> items, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : pageSize;

            var itemsToDisplay = items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return itemsToDisplay;
        }
    }
}
