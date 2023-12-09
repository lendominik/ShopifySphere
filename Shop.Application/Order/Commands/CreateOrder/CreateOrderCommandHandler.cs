using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using System.Security.Claims;
using System.Text;

namespace Shop.Application.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        
        private readonly IOrderRepository _orderRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateOrderCommandHandler(IHttpContextAccessor httpContextAccessor, IOrderRepository orderRepository, IItemRepository itemRepository, ICartService cartService,ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
            _itemRepository = itemRepository;
            _cartService = cartService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cartItems = _cartService.GetCartItems();

            if (cartItems == null || cartItems.Count == 0)
            {
                throw new NotFoundException("The cart is empty.");
            }

            foreach (var cartItem in cartItems)
            {
                await _cartItemRepository.Create(cartItem);
            }

            var order = _mapper.Map<Domain.Entities.Order>(request);

            order.CartItems = cartItems;
            order.CartTotal = CalculateCartTotal.Calculate(cartItems);
            order.OrderDate = DateTime.Now;
            order.Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            order.OrderStatus = OrderStatus.Pending;
            order.IsPaid = false;

            CheckStockQuantity(cartItems);

            _cartService.SaveCartItemsToSession(cartItems);

            await _orderRepository.Create(order);

            return Unit.Value;
        }

        private void CheckStockQuantity(IEnumerable<CartItem> cartItems)
        {
            foreach (var item in cartItems)
            {
                if (item.Item.StockQuantity < item.Quantity || item.Item.StockQuantity < 0 || item.Quantity <= 0)
                {
                    throw new OutOfStockException($"There are not enough items in stock for {item.Item.Name}.");
                }

                item.Item.StockQuantity -= item.Quantity;
            }
        }
    }
}
