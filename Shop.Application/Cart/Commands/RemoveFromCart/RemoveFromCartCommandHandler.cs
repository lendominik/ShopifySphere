using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Domain.Entities;
using System.Text;

namespace Shop.Application.Cart.Commands.RemoveFromCart
{
    public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartService _cartService;

        public RemoveFromCartCommandHandler(IHttpContextAccessor httpContextAccessor, ICartService cartService)
        {
            _httpContextAccessor = httpContextAccessor;
            _cartService = cartService;
        }

        public async Task<Unit> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var items = _cartService.GetCartItems();

            var cartItem = items.FirstOrDefault(i => i.Id == request.Id);

            if (cartItem == null)
            {
                throw new NotFoundException("Cart or user not found.");
            }

            items.Remove(cartItem);

            _cartService.SaveCartItemsToSession(items);

            return Unit.Value;
        }
    }
}
