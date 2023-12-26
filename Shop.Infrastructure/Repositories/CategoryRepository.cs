using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infrastructure.Persistence;

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
            => await _dbContext.Categories
            .AsNoTracking()
            .Select(c => new Category
            {  Description = c.Description, Name = c.Name, EncodedName = c.EncodedName })
            .ToListAsync();
        public async Task<Category> GetByEncodedName(string encodedName)
           => await _dbContext.Categories
            .FirstOrDefaultAsync(e => e.EncodedName == encodedName);
        public bool CategoryExists(string name)
            =>  _dbContext.Categories.Any(c => c.Name == name);
    }
}
