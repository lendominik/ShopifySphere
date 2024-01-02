using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Application.Services.CartServices;

namespace Shop.Application.Cart.Queries.GetCart
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartDto>
    {
        private readonly ICartRepositoryService _cartRepositoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartIdProviderService _cartIdProviderService;
        private readonly ICartCalculatorService _cartCalculatorService;

        public GetCartQueryHandler(ICartRepositoryService cartRepositoryService, ICartCalculatorService cartCalculatorService, ICartIdProviderService cartIdProviderService, IHttpContextAccessor httpContextAccessor)
        {
            _cartRepositoryService = cartRepositoryService;
            _httpContextAccessor = httpContextAccessor;
            _cartIdProviderService = cartIdProviderService;
            _cartCalculatorService = cartCalculatorService;
        }
        public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cartId = _cartIdProviderService.GetOrCreateCartId(_httpContextAccessor);

            var items = _cartRepositoryService.GetCartItems(_httpContextAccessor);
  
            var cartDto = new CartDto
            {
                CartItems = items,
                CartTotal = _cartCalculatorService.CalculateCartTotal(items),
                Id = cartId
            };
           
            return cartDto;
        }
    }
}
