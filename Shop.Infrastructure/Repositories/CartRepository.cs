using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
    public class CartRepository : ICartRepository
    {
        private readonly ShopDbContext _dbContext;

        public CartRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<CartItem>> GetCartItems(string cartId)
        {
            var cart = await _dbContext.CartItems.Where(c => c.CartId == cartId).ToListAsync();
            await Console.Out.WriteLineAsync("GetCartItems"); await Console.Out.WriteLineAsync("GetCartItems"); await Console.Out.WriteLineAsync("GETCART"); await Console.Out.WriteLineAsync("GETCART");
            return cart;
        }
        public async Task<Cart> GetCart(string cartId)
        {
            var cart = await _dbContext.Carts
            .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Item)
            .FirstOrDefaultAsync(c => c.Id == cartId);

            return cart;
        }
        public async Task AddToCart(Cart cart, CartItem cartItem)
        {
            cart.CartItems.Add(cartItem);
            await _dbContext.SaveChangesAsync();
        }
        public async Task SaveCartToDatabase(Cart cart)
        {
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<string> GetCartId(IHttpContextAccessor httpContextAccessor)
        {
            var session = httpContextAccessor.HttpContext.Session;
            var cartId = session.GetString("CartSessionKey");

            if (string.IsNullOrWhiteSpace(cartId))
            {
                Guid tempCartId = Guid.NewGuid();
                cartId = tempCartId.ToString();

                var newCart = new Cart
                {
                    Id = cartId,
                    CartTotal = 0
                };

                session.SetString("CartSessionKey", cartId);

                _dbContext.Carts.Add(newCart);
                await _dbContext.SaveChangesAsync();
            }

            return cartId;
        }
    }
}
