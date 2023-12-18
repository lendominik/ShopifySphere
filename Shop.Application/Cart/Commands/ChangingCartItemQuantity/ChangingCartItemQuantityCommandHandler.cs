﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Domain.Entities;
using System.Text;

namespace Shop.Application.Cart.Commands.ChangingCartItemQuantity
{
    public class ChangingCartItemQuantityCommandHandler : IRequestHandler<ChangingCartItemQuantityCommand>
    {
        private readonly ICartService _cartService;

        public ChangingCartItemQuantityCommandHandler(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<Unit> Handle(ChangingCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var items = _cartService.GetCartItems();

            var item = items.LastOrDefault(i => i.Id == request.Id);

            if(item == null)
            {
                throw new NotFoundException("Item not found.");
            }

            _cartService.UpdateCartItemPriceAndQuantity(item, request.Quantity);

            _cartService.SaveCartItemsToSession(items);

            return Unit.Value;
        }
        
    }
}
