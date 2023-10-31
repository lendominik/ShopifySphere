using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart.Commands.RemoveFromCart
{
    public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RemoveFromCartCommandHandler(ICartItemRepository cartItemRepository, ICartRepository cartRepository, IHttpContextAccessor httpContextAccessor)
        {
            _cartItemRepository = cartItemRepository;
            _cartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var cartItem = await _cartItemRepository.GetCartItem(request.Id);
            var cartId = await _cartRepository.GetCartId(_httpContextAccessor);
            var cart = await _cartRepository.GetCart(cartId);

            cart.CartTotal = cart.CartTotal - cartItem.UnitPrice;
            await _cartRepository.Commit(); 

            await _cartItemRepository.Delete(cartItem);

            return Unit.Value;
        }
    }
}
