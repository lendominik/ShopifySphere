using Shop.Domain.Entities;

namespace Shop.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();
        Task Create(Category category);
        Task Delete(Category category);
        Task<Category> GetByEncodedName(string encodedName);
        Task<Category> GetByName(string name);
        Task Commit();
    }
}
