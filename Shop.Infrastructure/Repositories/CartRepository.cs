using Microsoft.AspNetCore.Http;
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
        public async Task AddToCart(Cart cart, CartItem cartItem)
        {
            cart.CartItems.Add(cartItem);
            await _dbContext.SaveChangesAsync();
        }

        
    }
}
