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
        Task AddToCart(Cart cart, CartItem cartItem);
        Task<Cart> GetCart(string cartId);
        Task<string?> GetCartId(IHttpContextAccessor httpContextAccessor);
        Task SaveCartToDatabase(Cart cart);
        Task Commit();
    }
}
