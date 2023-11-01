using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.Exceptions;

namespace Shop.Application.Cart.Commands.AddToCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IItemRepository _itemRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public AddToCartCommandHandler(IItemRepository itemRepository, ICartItemRepository cartItemRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _itemRepository = itemRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var cartId = await _cartItemRepository.GetCartId(_httpContextAccessor);
            var cartItems = await _cartItemRepository.GetCartItems(cartId);
            var item = await _itemRepository.GetByEncodedName(request.EncodedName);

            if(cartId == null || item == null)
            {
                throw new NotFoundException("Nie znaleziono kosza użytkownika lub podanego przedmiotu.");
            }

            if(request.Quantity > item.StockQuantity)
            {
                throw new OutOfStockException("Nie ma tylu przedmiotów w magazynie.");
            }

            var existingCartItem = cartItems.FirstOrDefault(ci => ci.ItemId == item.Id);

            if (existingCartItem != null)
            {
                var cartItem = await _cartItemRepository.GetCartItem(existingCartItem.Id);
                cartItem.Quantity = existingCartItem.Quantity + 1;
                cartItem.UnitPrice = cartItem.Quantity * item.Price;
                await _cartItemRepository.Commit();
            }
            else
            {
                var cartItem = new CartItem
                {
                    Item = item,
                    CartId = cartId,
                    Quantity = 1,
                    UnitPrice = 1 * item.Price,
                    ItemId = item.Id
                };
                await _cartItemRepository.AddToCart(cartItem);
            }

            return Unit.Value;
        }
    }
}
