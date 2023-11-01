using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Application.Exceptions;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RemoveFromCartCommandHandler(ICartItemRepository cartItemRepository, IHttpContextAccessor httpContextAccessor)
        {
            _cartItemRepository = cartItemRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var cartItem = await _cartItemRepository.GetCartItem(request.Id);
            var cartId = await _cartItemRepository.GetCartId(_httpContextAccessor);

            if (cartId == null || cartItem == null)
            {
                throw new NotFoundException("Nie znaleziono kosza użytkownika lub podanego przedmiotu.");
            }

            await _cartItemRepository.Delete(cartItem);

            return Unit.Value;
        }
    }
}
