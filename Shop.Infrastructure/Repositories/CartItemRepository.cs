using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ShopDbContext _dbContext;

        public CartItemRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(CartItem cartItem)
        {
            _dbContext.CartItems.Add(cartItem);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<CartItem> GetCartItem(int cartItemId)
        {
            var cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);

            return cartItem;
        }
        public async Task Delete(CartItem cartItem)
        {
            _dbContext.CartItems.Remove(cartItem);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCartItemQuantity(CartItem cartItem, int quantity)
        {
            cartItem.Quantity = quantity;
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateCartItem(CartItem cartItem)
        { 

            _dbContext.CartItems.Update(cartItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
