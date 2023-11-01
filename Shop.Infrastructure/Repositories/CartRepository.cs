using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddToCart(CartItem cartItem)
        {
            if (cartItem == null)
            {
                throw new ArgumentNullException(nameof(cartItem));
            }

            _dbContext.CartItems.Add(cartItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetCartId(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            var session = httpContextAccessor.HttpContext.Session;
            var cartId = session.GetString("CartSessionKey");

            if (string.IsNullOrWhiteSpace(cartId))
            {
                cartId = Guid.NewGuid().ToString();
                session.SetString("CartSessionKey", cartId);
            }

            return cartId;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}