using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Services
{
    public interface ICartService
    {
        List<CartItem> GetCartItems();
        string GetOrCreateCartId();
        string GetCart();
        void SaveCartItemsToSession(List<CartItem> items);
        decimal CalculateCartTotal(List<CartItem> cartItems);
    }

    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetOrCreateCartId()
        {
            var session = _httpContextAccessor.HttpContext.Session;

            var cartId = session.GetString("CartSessionKey");

            if (string.IsNullOrWhiteSpace(cartId))
            {
                cartId = Guid.NewGuid().ToString();
                session.SetString("CartSessionKey", cartId);
            }

            return cartId;
        }

        public string GetCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cart = session.GetString("Cart");
            return cart;
        }

        public List<CartItem> GetCartItems()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cart = session.GetString("Cart");
            var items = new List<CartItem>();

            if (cart != null)
            {
                items = JsonConvert.DeserializeObject<List<CartItem>>(cart);
            }

            return items;
        }

        public void SaveCartItemsToSession(List<CartItem> items)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var serializedCartItems = JsonConvert.SerializeObject(items);
            session.SetString("Cart", serializedCartItems);
        }
        public decimal CalculateCartTotal(List<CartItem> cartItems)
        {
            decimal total = 0;
            foreach (var cartItem in cartItems)
            {
                total += cartItem.UnitPrice;
            }
            return total;
        }
    }
}
