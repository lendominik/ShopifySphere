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

        public async Task<CartItem> GetCartItem(Guid cartItemId)
        {

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
        public async Task<List<CartItem>> GetCartItems(string cartId)
        {
            if (string.IsNullOrWhiteSpace(cartId))
            {
                throw new ArgumentException("Invalid cartId");
            }

            var cartItems = await _dbContext.CartItems
                .Include(c => c.Item)
                .Where(c => c.CartId == cartId)
                .ToListAsync();

            return cartItems;
        }
        public async Task AddCartItems(List<CartItem> cartItems)
        {
            _dbContext.CartItems.AddRange(cartItems);
            await _dbContext.SaveChangesAsync();
        }
        public async Task AddToCart(CartItem cartItem)
        {
            if (cartItem == null)
            {
                throw new ArgumentNullException(nameof(cartItem));
            }

            _dbContext.CartItems.Add(cartItem);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveCartItemsByCartId(string cartId)
        {
            var cartItems = await _dbContext.CartItems
                .Where(c => c.CartId == cartId)
                .ToListAsync();

            if (cartItems != null && cartItems.Any())
            {
                _dbContext.CartItems.RemoveRange(cartItems);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<CartItem>> GetCartItemsByOrderId(int orderId)
        {
            var cartItems = await _dbContext.CartItems.Include(ci => ci.Item).Where(c => c.CartId == orderId.ToString()).ToListAsync();

            return cartItems;
        }
    }
}