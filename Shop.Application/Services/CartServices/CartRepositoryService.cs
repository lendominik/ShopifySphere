using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Entities;

namespace Shop.Application.Services.CartServices
{
    public interface ICartRepositoryService
    {
        List<CartItem> GetCartItems(IHttpContextAccessor httpContextAccessor);
        string GetCart(IHttpContextAccessor httpContextAccessor);
        void SaveCartItemsToSession(List<CartItem> items, IHttpContextAccessor httpContextAccessor);
    }
    public class CartRepositoryService : ICartRepositoryService
    {
        public string GetCart(IHttpContextAccessor httpContextAccessor)
        {
            var session = httpContextAccessor.HttpContext.Session;
            var cart = session.GetString("Cart");
            return cart;
        }
        public List<CartItem> GetCartItems(IHttpContextAccessor httpContextAccessor)
        {
            var session = httpContextAccessor.HttpContext.Session;
            var cart = session.GetString("Cart");
            var items = new List<CartItem>();

            if (cart != null)
            {
                items = JsonConvert.DeserializeObject<List<CartItem>>(cart);
            }

            return items;
        }
        public void SaveCartItemsToSession(List<CartItem> items, IHttpContextAccessor httpContextAccessor)
        {
            var session = httpContextAccessor.HttpContext.Session;
            var serializedCartItems = JsonConvert.SerializeObject(items);
            session.SetString("Cart", serializedCartItems);
        }
    }
}
