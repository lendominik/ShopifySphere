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
        Task Delete(CartItem cartItem);
        Task UpdateCartItemQuantity(CartItem cartItem, int quantity);
        Task UpdateCartItem(CartItem cartItem);
        Task Commit();
    }
}
