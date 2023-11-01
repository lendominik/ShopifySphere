using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Order.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor,ICartRepository cartRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cartId = await _cartRepository.GetCartId(_httpContextAccessor);
            var cartItems = await _cartRepository.GetCartItems(cartId);

            var order = _mapper.Map<Domain.Entities.Order>(request);

            order.CartItems = cartItems;
            order.CartTotal = CalculateCartTotal(cartItems);
            order.OrderDate = DateTime.Now;

            await _orderRepository.Create(order);
            await _cartRepository.RemoveCartItemsByCartId(cartId);

            return Unit.Value;
        }
        public decimal CalculateCartTotal(List<CartItem> cartItems)
        {
            if (cartItems == null)
            {
                return 0; 
            }

            decimal total = cartItems.Sum(item => item.UnitPrice);

            return total;
        }
    }
}
