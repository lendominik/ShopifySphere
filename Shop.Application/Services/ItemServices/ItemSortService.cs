using System.Linq.Expressions;

namespace Shop.Application.Services.ItemServices
{
    public interface IItemSortService
    {
        IQueryable<Domain.Entities.Item> SortItems(IQueryable<Domain.Entities.Item> items, string sortBy, string sortDirection);
    }

    public class ItemSortService : IItemSortService
    {
        public IQueryable<Domain.Entities.Item> SortItems(IQueryable<Domain.Entities.Item> items, string sortBy, string sortDirection)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Domain.Entities.Item, object>>>
            {
                {nameof(Domain.Entities.Item.Name), x => x.Name},
                {nameof(Domain.Entities.Item.Category), x => x.Category.Name},
                {nameof(Domain.Entities.Item.Price), x => x.Price},
            };

                if (columnsSelectors.TryGetValue(sortBy, out var selectedColumn))
                {
                    items = sortDirection == "ASC" ? items.AsQueryable().OrderBy(selectedColumn) : items.AsQueryable().OrderByDescending(selectedColumn);
                }
            }

            return items;
        }
    }
}
