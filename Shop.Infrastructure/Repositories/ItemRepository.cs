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

        public async Task<IEnumerable<Item>> GetAll()
            => await _dbContext.Items.ToListAsync();
    }
}
