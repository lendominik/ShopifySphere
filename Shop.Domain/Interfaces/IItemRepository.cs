using Shop.Domain.Entities;

namespace Shop.Domain.Interfaces
{
    public interface IItemRepository
    {
        IQueryable<Item> GetAll();
        Task Create(Item item);
        Task<Item> GetByEncodedName(string encodedName);
        bool ItemExists(string name);
        Task Commit();
        Task Delete(Item item);
    }
}
