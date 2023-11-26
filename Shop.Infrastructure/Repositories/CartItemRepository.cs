using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ShopDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartItemRepository(ShopDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Create(CartItem cartItem)
        {
            _dbContext.Items.Attach(cartItem.Item);
            _dbContext.CartItems.Add(cartItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}