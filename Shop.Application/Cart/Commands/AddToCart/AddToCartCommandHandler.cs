using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Text;
using Shop.Application.Exceptions;
using Newtonsoft.Json;

namespace Shop.Application.Cart.Commands.AddToCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IItemRepository _itemRepository;

        public AddToCartCommandHandler(IHttpContextAccessor httpContextAccessor, IItemRepository itemRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _itemRepository = itemRepository;
        }

        public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            if (_httpContextAccessor == null)
            {
                throw new ArgumentNullException(nameof(_httpContextAccessor));
            }

            var session = _httpContextAccessor.HttpContext.Session;
            var cartId = session.GetString("CartSessionKey");

            if (string.IsNullOrWhiteSpace(cartId))
            {
                cartId = Guid.NewGuid().ToString();
                session.SetString("CartSessionKey", cartId);
            }

            var cart = session.GetString("Cart");
            var items = new List<CartItem>();

            if(cart != null )
            {
               items = JsonConvert.DeserializeObject<List<CartItem>>(cart);
            }
            
            var item = await _itemRepository.GetByEncodedName(request.EncodedName);

            if (items == null)
            {
                items = new List<CartItem>();
            }

            if(item == null)
            {
                throw new NotFoundException("Nie znaleziono podanego przedmiotu.");
            }

            if (request.Quantity > item.StockQuantity || item.StockQuantity < 1)
            {
                throw new OutOfStockException("Nie ma tylu przedmiotów w magazynie.");
            }

            var existingCartItem = items.FirstOrDefault(ci => ci.ItemId == item.Id && ci.CartId == cartId);

            if(existingCartItem != null)
            {
                existingCartItem.Quantity = existingCartItem.Quantity + 1;
                existingCartItem.UnitPrice = item.Price * existingCartItem.Quantity;
                var serializedCartItems = JsonConvert.SerializeObject(items);
                session.SetString("Cart", serializedCartItems);
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

                var serializedCartItems = JsonConvert.SerializeObject(items);
                session.SetString("Cart", serializedCartItems);
            }

            return Unit.Value;
        }
    }
}
