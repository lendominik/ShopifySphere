﻿using MediatR;
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
        private readonly ICartRepository _cartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IItemRepository _itemRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public AddToCartCommandHandler(ICartRepository cartRepository, IItemRepository itemRepository, ICartItemRepository cartItemRepository, IHttpContextAccessor httpContextAccessor)
        {
            _cartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
            _itemRepository = itemRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var cartId = await _cartRepository.GetCartId(_httpContextAccessor);
            var cart = await _cartRepository.GetCart(cartId);
            var item = await _itemRepository.GetByEncodedName(request.EncodedName);

            if(cartId == null || cart == null || item == null)
            {
                throw new NotFoundException("Nie znaleziono kosza użytkownika lub podanego przedmiotu.");
            }

            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ItemId == item.Id);

            if (existingCartItem != null)
            {
                var cartItem = await _cartItemRepository.GetCartItem(existingCartItem.Id);
                cartItem.Quantity = existingCartItem.Quantity + 1;
                cartItem.UnitPrice = cartItem.Quantity * item.Price;
                cart.CartTotal = item.Price * cartItem.Quantity;
                await _cartRepository.Commit();
            }
            else
            {
                cart.CartTotal = cart.CartTotal + item.Price;

                var cartItem = new CartItem
                {
                    Cart = cart,
                    Item = item,
                    CartId = cartId,
                    Quantity = 1,
                    UnitPrice = 1 * item.Price,
                    ItemId = item.Id
                };
                await _cartRepository.AddToCart(cart, cartItem);
            }

            return Unit.Value;
        }
    }
}
