using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Application.Exceptions;
using Shop.Application.Services.CartServices;
using Shop.Application.Services.OrderServices;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using System.Security.Claims;
using System.Text;

namespace Shop.Application.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _email;
        private readonly ICartRepositoryService _cartRepositoryService;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler(ICartItemRepository cartItemRepository, IHttpContextAccessor httpContextAccessor, IOrderService orderService, IOrderRepository orderRepository, ICartRepositoryService cartRepositoryService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _orderService = orderService;
            _email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            _cartRepositoryService = cartRepositoryService;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cartItems = _cartRepositoryService.GetCartItems(_httpContextAccessor);

            if (!cartItems.Any())
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

            _cartRepositoryService.SaveCartItemsToSession(cartItems, _httpContextAccessor);

            await _orderRepository.Create(order);

            return Unit.Value;
        }

    }
}
