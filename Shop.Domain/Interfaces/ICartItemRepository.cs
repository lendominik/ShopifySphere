using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface ICartItemRepository
    {
        Task Create(Domain.Entities.CartItem cartItem);
        Task<CartItem> GetCartItem(int cartItemId);
        Task<List<CartItem>> GetCartItemsByOrderId(int orderId);
        Task Delete(CartItem cartItem);
        Task UpdateCartItemQuantity(CartItem cartItem, int quantity);
        Task UpdateCartItem(CartItem cartItem);
        Task Commit();
        Task AddToCart(CartItem cartItem);
        Task RemoveCartItemsByCartId(string cartId);
        Task<List<CartItem>> GetCartItems(string cartId);
        Task<string?> GetCartId(IHttpContextAccessor httpContextAccessor);
        Task<decimal> CalculateCartTotal(List<CartItem> cartItems);
    }
}
