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

        public Task<CartItem> GetCartItem(int cartId)
        {
            var cartItem = _dbContext.CartItems.FirstOrDefaultAsync(c => c.Id == cartId);

            return cartItem;
        }
    }
}
