using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<Item>> GetAll()
            => await _dbContext.Items.Include(c => c.Category).ToListAsync();

        public async Task<Item> GetByEncodedName(string encodedName)
        {
            var item = await _dbContext.Items.FirstOrDefaultAsync(e => e.EncodedName == encodedName);

            return item;
        }
        public async Task<Item> GetByName(string name)
        {
            var item = await _dbContext.Items.FirstOrDefaultAsync(e => e.Name == name);

            return item;
        }
        public async Task Delete(Item item)
        {
            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
        
    }
}
