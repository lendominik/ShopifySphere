namespace Shop.Application.Services
{
    public interface IItemFilterService
    {
        IQueryable<Domain.Entities.Item> FilterByCategory(IQueryable<Domain.Entities.Item> items, string selectedCategory);
        IQueryable<Domain.Entities.Item> FilterBySearchPhrase(IQueryable<Domain.Entities.Item> items, string phrase);
    }

    public class ItemFilterService : IItemFilterService
    {
        public IQueryable<Domain.Entities.Item> FilterByCategory(IQueryable<Domain.Entities.Item> items, string selectedCategory)
        {
            if (!string.IsNullOrEmpty(selectedCategory))
            {
                items = items.Where(c => c.Category.Name == selectedCategory);
            }

            return items;
        }

        public IQueryable<Domain.Entities.Item> FilterBySearchPhrase(IQueryable<Domain.Entities.Item> items, string phrase)
        {
            if (!string.IsNullOrEmpty(phrase))
            {
                items = items.Where(r => r.Name.ToLower().Contains(phrase.ToLower()) || r.Description.ToLower().Contains(phrase.ToLower()));
            }

            return items;
        }
        
    }
}
