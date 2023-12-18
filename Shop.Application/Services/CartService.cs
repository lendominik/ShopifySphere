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
        void UpdateOrCreateCartItem(Domain.Entities.Item item, string cartId, List<CartItem> items);
        void UpdateCartItemPriceAndQuantity(CartItem item, int newQuantity);
    }

    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void UpdateOrCreateCartItem(Domain.Entities.Item item, string cartId, List<CartItem> items)
        {
            var existingCartItem = items.FirstOrDefault(i => i.ItemId == item.Id);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity = existingCartItem.Quantity + 1;
                existingCartItem.UnitPrice = item.Price * existingCartItem.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    Item = item,
                    CartId = cartId,
                    Quantity = 1,
                    UnitPrice = 1 * item.Price,
                    ItemId = item.Id,
                };
                items.Add(cartItem);
            }

            SaveCartItemsToSession(items);
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
        public void UpdateCartItemPriceAndQuantity(CartItem item, int newQuantity)
        {
            decimal unitPricePerItem = item.UnitPrice / item.Quantity;
            item.Quantity = newQuantity;
            item.UnitPrice = unitPricePerItem * newQuantity;
        }
    }
}
