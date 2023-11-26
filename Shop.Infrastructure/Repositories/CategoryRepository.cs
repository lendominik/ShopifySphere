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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopDbContext _dbContext;

        public CategoryRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Delete(Category category)
        {
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Category>> GetAll()
            => await _dbContext.Categories.ToListAsync();
        public async Task<Category> GetByEncodedName(string encodedName)
           => await _dbContext.Categories.FirstAsync(e => e.EncodedName == encodedName);
        public async Task<Category?> GetByName(string name)
            => await _dbContext.Categories.FirstOrDefaultAsync(e => e.Name == name);        
    }
}
