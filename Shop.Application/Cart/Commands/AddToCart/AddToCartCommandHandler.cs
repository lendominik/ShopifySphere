using MediatR;
using Shop.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application.Exceptions;
using Shop.Application.Services.CartServices;

namespace Shop.Application.Cart.Commands.AddToCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartIdProviderService _cartIdProvider;
        private readonly ICartRepositoryService _cartRepositoryService;
        private readonly ICartUpdaterService _cartUpdaterService;

        public AddToCartCommandHandler(ICartUpdaterService cartUpdaterService, ICartIdProviderService cartIdProvider, ICartRepositoryService cartRepositoryService, IItemRepository itemRepository, IHttpContextAccessor httpContextAccessor)
        {
            _itemRepository = itemRepository;
            _httpContextAccessor = httpContextAccessor;
            _cartIdProvider = cartIdProvider;
            _cartRepositoryService = cartRepositoryService;
            _cartUpdaterService = cartUpdaterService;
        }

        public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var cartId = _cartIdProvider.GetOrCreateCartId(_httpContextAccessor);

            var items = _cartRepositoryService.GetCartItems(_httpContextAccessor);

            var item = await _itemRepository.GetByEncodedName(request.EncodedName);

            if(item == null)
            {
                throw new NotFoundException("Item not found.");
            }

            if (request.Quantity > item.StockQuantity || item.StockQuantity < 1)
            {
                throw new OutOfStockException("There are not that many items in stock.");
            }

            _cartUpdaterService.UpdateOrCreateCartItem(item, cartId, items, _httpContextAccessor);

            return Unit.Value;
        }
    }
}
