using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task AddToCart(CartItem cartItem);
        
        Task<List<CartItem>> GetCartItems(string cartId);
        //Task<Cart> GetCart(string cartId);
        Task<string?> GetCartId(IHttpContextAccessor httpContextAccessor);
        Task Commit();
    }
}
