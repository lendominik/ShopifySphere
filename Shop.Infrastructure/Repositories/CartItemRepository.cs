﻿using Microsoft.AspNetCore.Http;
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
        public async Task<decimal> CalculateCartTotal(List<CartItem> cartItems)
        {
            if (cartItems == null)
            {
                return 0;
            }

            decimal total = cartItems.Sum(item => item.UnitPrice);

            return total;
        }
    }
}