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
        private readonly IItemRepository _itemRepository;
        private readonly ICartService _cartService;

        public AddToCartCommandHandler(IItemRepository itemRepository, ICartService cartService)
        {
            _itemRepository = itemRepository;
            _cartService = cartService;
        }

        public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var cartId = _cartService.GetOrCreateCartId();

            var items = _cartService.GetCartItems();

            var item = await _itemRepository.GetByEncodedName(request.EncodedName);

            if(item == null)
            {
                throw new NotFoundException("Item not found.");
            }

            if (request.Quantity > item.StockQuantity || item.StockQuantity < 1)
            {
                throw new OutOfStockException("There are not that many items in stock.");
            }

            _cartService.UpdateOrCreateCartItem(item, cartId, items);

            return Unit.Value;
        }
    }
}
