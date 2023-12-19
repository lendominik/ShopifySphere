using Microsoft.AspNetCore.Http;
using Shop.Application.Exceptions;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
    public interface IOrderService
    {
        decimal Calculate(List<CartItem> cartItems);
        void CheckStockQuantity(IEnumerable<CartItem> cartItems);
        Domain.Entities.Order CreateOrderFromCart(Domain.Entities.Order order, List<CartItem> cartItems, string email);
    }

    public class OrderService : IOrderService
    {
        public decimal Calculate(List<CartItem> cartItems)
        {
            if (!cartItems.Any())
            {
                return 0;
            }

            return cartItems.Sum(item => item.UnitPrice);
        }
        public void CheckStockQuantity(IEnumerable<CartItem> cartItems)
        {
            if (cartItems.All(ci => ci.Item.StockQuantity >= ci.Quantity && ci.Quantity > 0))
            {
                foreach (var cartItem in cartItems)
                {
                    cartItem.Item.StockQuantity -= cartItem.Quantity;
                }
            }
            else
            {
                throw new OutOfStockException("There are not enough items in stock.");
            }
        }
        public Domain.Entities.Order CreateOrderFromCart(Domain.Entities.Order order, List<CartItem> cartItems, string email)
        {
            order.CartItems = cartItems;
            order.CartTotal = Calculate(cartItems);
            order.Email = email;

            return order;
        }
    }
}
