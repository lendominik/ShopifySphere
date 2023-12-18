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
        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler(IHttpContextAccessor httpContextAccessor, IOrderService orderService, IOrderRepository orderRepository, IItemRepository itemRepository, ICartService cartService,ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
            _itemRepository = itemRepository;
            _cartService = cartService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _orderService = orderService;
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
            order.CartTotal = _orderService.Calculate(cartItems);
            order.Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

            _orderService.CheckStockQuantity(cartItems);

            _cartService.SaveCartItemsToSession(cartItems);

            await _orderRepository.Create(order);

            return Unit.Value;
        }

    }
}
