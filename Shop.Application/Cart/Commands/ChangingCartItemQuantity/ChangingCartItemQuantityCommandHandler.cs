using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Exceptions;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart.Commands.ChangingCartItemQuantity
{
    public class ChangingCartItemQuantityCommandHandler : IRequestHandler<ChangingCartItemQuantityCommand>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChangingCartItemQuantityCommandHandler(ICartItemRepository cartItemRepository, ICartRepository cartRepository, IHttpContextAccessor httpContextAccessor)
        {
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Unit> Handle(ChangingCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var cartItem = await _cartItemRepository.GetCartItem(request.Id);
            var cartId = await _cartRepository.GetCartId(_httpContextAccessor);

            if (cartId == null || cartItem == null)
            {
                throw new NotFoundException("Nie znaleziono kosza użytkownika lub podanego przedmiotu.");
            }

            if (request.Quantity > cartItem.Item.StockQuantity)
            {
                throw new OutOfStockException("Nie ma tylu przedmiotów w magazynie.");
            }

            decimal unitPrice = cartItem.UnitPrice / cartItem.Quantity;

            cartItem.Quantity = request.Quantity;

            cartItem.UnitPrice = unitPrice * request.Quantity;

            await _cartItemRepository.UpdateCartItem(cartItem);

            return Unit.Value;
        }
        
    }
}
