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
        Domain.Entities.Order CreateOrderFromCart(Domain.Entities.Order order, List<CartItem> cartItems);
    }

    public class OrderService : IOrderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public decimal Calculate(List<CartItem> cartItems)
        {
            if (cartItems == null)
            {
                return 0;
            }

            decimal total = cartItems.Sum(item => item.UnitPrice);

            return total;
        }
        public void CheckStockQuantity(IEnumerable<CartItem> cartItems)
        {
            foreach (var item in cartItems)
            {
                if (item.Item.StockQuantity < item.Quantity || item.Item.StockQuantity < 0 || item.Quantity <= 0)
                {
                    throw new OutOfStockException($"There are not enough items in stock for {item.Item.Name}.");
                }

                item.Item.StockQuantity -= item.Quantity;
            }
        }
        public Domain.Entities.Order CreateOrderFromCart(Domain.Entities.Order order, List<CartItem> cartItems)
        {
            order.CartItems = cartItems;
            order.CartTotal = Calculate(cartItems);
            order.Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

            return order;
        }
    }
}
