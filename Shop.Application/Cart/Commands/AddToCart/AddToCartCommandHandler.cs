using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text;
using Shop.Application.Exceptions;
using Newtonsoft.Json;
using Shop.Application.Services;

namespace Shop.Application.Cart.Commands.AddToCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IItemRepository _itemRepository;
        private readonly ICartService _cartService;

        public AddToCartCommandHandler(IHttpContextAccessor httpContextAccessor, IItemRepository itemRepository, ICartService cartService)
        {
            _httpContextAccessor = httpContextAccessor;
            _itemRepository = itemRepository;
            _cartService = cartService;
        }

        public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var cartId = _cartService.GetOrCreateCartId();
            var cart = _cartService.GetCart();
            var items = new List<CartItem>();

            if(cart != null )
            {
                items = _cartService.GetCartItems();
            }
            
            var item = await _itemRepository.GetByEncodedName(request.EncodedName);

            if (items == null)
            {
                items = new List<CartItem>();
            }

            if(item == null)
            {
                throw new NotFoundException("Item not found.");
            }

            if (request.Quantity > item.StockQuantity || item.StockQuantity < 1)
            {
                throw new OutOfStockException("There are not that many items in stock.");
            }

            var existingCartItem = items.FirstOrDefault(ci => ci.ItemId == item.Id && ci.CartId == cartId);

            if(existingCartItem != null)
            {
                existingCartItem.Quantity = existingCartItem.Quantity + 1;
                existingCartItem.UnitPrice = item.Price * existingCartItem.Quantity;

                _cartService.SaveCartItemsToSession(items);
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

                _cartService.SaveCartItemsToSession(items);
            }

            return Unit.Value;
        }
    }
}
