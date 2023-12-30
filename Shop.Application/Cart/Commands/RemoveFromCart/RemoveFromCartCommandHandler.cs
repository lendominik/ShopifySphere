using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Application.Exceptions;
using Shop.Application.Services.CartServices;
using Shop.Domain.Entities;
using System.Text;

namespace Shop.Application.Cart.Commands.RemoveFromCart
{
    public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand>
    {
        private readonly ICartRepositoryService _cartRepositoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RemoveFromCartCommandHandler(ICartRepositoryService cartRepositoryService, IHttpContextAccessor httpContextAccessor)
        {
            _cartRepositoryService = cartRepositoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var items = _cartRepositoryService.GetCartItems(_httpContextAccessor);

            var cartItem = items.FirstOrDefault(i => i.Id == request.Id);

            if (cartItem == null)
            {
                throw new NotFoundException("Cart or user not found.");
            }

            items.Remove(cartItem);

            _cartRepositoryService.SaveCartItemsToSession(items, _httpContextAccessor);

            return Unit.Value;
        }
    }
}
