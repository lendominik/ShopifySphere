using MediatR;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChangingCartItemQuantityCommandHandler(ICartService cartService, IHttpContextAccessor httpContextAccessor)
        {
            _cartService = cartService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Unit> Handle(ChangingCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var items = _cartService.GetCartItems(_httpContextAccessor);

            var item = items.LastOrDefault(i => i.Id == request.Id);

            if(item == null)
            {
                throw new NotFoundException("Item not found.");
            }

            _cartService.UpdateCartItemPriceAndQuantity(item, request.Quantity);

            _cartService.SaveCartItemsToSession(items, _httpContextAccessor);

            return Unit.Value;
        }
        
    }
}
