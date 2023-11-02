using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Application.Exceptions;
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
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor,ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var cartId = await _cartItemRepository.GetCartId(_httpContextAccessor);
            var cartItems = await _cartItemRepository.GetCartItems(cartId);

            if (cartItems == null)
            {
                throw new NotFoundException("Koszyk jest pusty.");
            }

            var order = _mapper.Map<Domain.Entities.Order>(request);

            order.CartItems = cartItems;
            order.CartTotal = CalculateCartTotal(cartItems);
            order.OrderDate = DateTime.Now;

            //MECHANIZM ODEJMOWANIA PRZEDMIOTOW ZAMOWIONYCH Z MAGAZYNNU 
            //trzeba uwzględnić, że zamówienie nie mogło nie zostać opłacone - czyli najpierw PayPal trzeba podpiąć

            await _orderRepository.Create(order);
            await _cartItemRepository.RemoveCartItemsByCartId(cartId);

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
