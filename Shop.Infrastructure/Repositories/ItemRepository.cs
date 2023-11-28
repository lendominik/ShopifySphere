using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ShopDbContext _dbContext;

        public ItemRepository(ShopDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task Create(Item item)
        {
            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task Delete(Item item)
        {
            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Item>> GetAll()
            => await _dbContext.Items.Include(c => c.Category).ToListAsync();
        public async Task<Item> GetByEncodedName(string encodedName)
            => await _dbContext.Items.FirstOrDefaultAsync(e => e.EncodedName == encodedName);
        public async Task<Item> GetByName(string name)
            => await _dbContext.Items.FirstOrDefaultAsync(e => e.Name == name); 
    }
}
