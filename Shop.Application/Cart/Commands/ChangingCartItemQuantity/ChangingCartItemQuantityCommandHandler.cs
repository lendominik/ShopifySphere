using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Application.Exceptions;
using Shop.Application.Services.CartServices;

namespace Shop.Application.Cart.Commands.ChangingCartItemQuantity
{
    public class ChangingCartItemQuantityCommandHandler : IRequestHandler<ChangingCartItemQuantityCommand>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartRepositoryService _cartRepositoryService;
        private readonly ICartCalculatorService _cartCalculatorService;

        public ChangingCartItemQuantityCommandHandler(IHttpContextAccessor httpContextAccessor, ICartRepositoryService cartRepositoryService, ICartCalculatorService cartCalculatorService)
        {
            _httpContextAccessor = httpContextAccessor;
            _cartRepositoryService = cartRepositoryService;
            _cartCalculatorService = cartCalculatorService;
        }
        public async Task<Unit> Handle(ChangingCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var items = _cartRepositoryService.GetCartItems(_httpContextAccessor);

            var item = items.LastOrDefault(i => i.Id == request.Id);

            if(item == null)
            {
                throw new NotFoundException("Item not found.");
            }

            _cartCalculatorService.UpdateCartItemPriceAndQuantity(item, request.Quantity);

            _cartRepositoryService.SaveCartItemsToSession(items, _httpContextAccessor);

            return Unit.Value;
        }
        
    }
}
