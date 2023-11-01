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
            if (cartItem == null)
            {
                throw new ArgumentNullException(nameof(cartItem));
            }

            _dbContext.CartItems.Add(cartItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CartItem> GetCartItem(int cartItemId)
        {
            if (cartItemId <= 0)
            {
                throw new ArgumentException("Invalid cartItemId");
            }

            var cartItem = await _dbContext.CartItems
                .Include(c => c.Item)
                .FirstOrDefaultAsync(c => c.Id == cartItemId);

            return cartItem;
        }

        public async Task Delete(CartItem cartItem)
        {
            if (cartItem == null)
            {
                throw new ArgumentNullException(nameof(cartItem));
            }

            _dbContext.CartItems.Remove(cartItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCartItemQuantity(CartItem cartItem, int quantity)
        {
            if (cartItem == null)
            {
                throw new ArgumentNullException(nameof(cartItem));
            }

            cartItem.Quantity = quantity;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCartItem(CartItem cartItem)
        {
            if (cartItem == null)
            {
                throw new ArgumentNullException(nameof(cartItem));
            }

            _dbContext.CartItems.Update(cartItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}