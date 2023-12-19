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
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _email;
        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler(IHttpContextAccessor httpContextAccessor, IOrderService orderService, IOrderRepository orderRepository, ICartService cartService,ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
            _cartService = cartService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _orderService = orderService;
            _email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cartItems = _cartService.GetCartItems(_httpContextAccessor);

            if (cartItems == null || cartItems.Count == 0)
            {
                throw new NotFoundException("The cart is empty.");
            }

            foreach (var cartItem in cartItems)
            {
                await _cartItemRepository.Create(cartItem);
            }

            var order = _mapper.Map<Domain.Entities.Order>(request);

            order = _orderService.CreateOrderFromCart(order, cartItems, _email);

            _orderService.CheckStockQuantity(cartItems);

            _cartService.SaveCartItemsToSession(cartItems, _httpContextAccessor);

            await _orderRepository.Create(order);

            return Unit.Value;
        }

    }
}
