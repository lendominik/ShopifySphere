using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;

namespace Shop.Application.Services.CartServices
{
    public interface ICartUpdaterService
    {
        void UpdateOrCreateCartItem(Domain.Entities.Item item, string cartId, List<CartItem> items, IHttpContextAccessor httpContextAccessor);
    }
    public class CartUpdaterService : ICartUpdaterService
    {
        private readonly ICartRepositoryService _cartRepositoryService;

        public CartUpdaterService(ICartRepositoryService cartRepositoryService)
        {
            _cartRepositoryService = cartRepositoryService;
        }
        public void UpdateOrCreateCartItem(Domain.Entities.Item item, string cartId, List<CartItem> items, IHttpContextAccessor httpContextAccessor)
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

            _cartRepositoryService.SaveCartItemsToSession(items, httpContextAccessor);
        }
    }
}
