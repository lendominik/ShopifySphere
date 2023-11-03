using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Order.Queries.OrderDetails
{
    public class OrderDetailsQueryHandler : IRequestHandler<OrderDetailsQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public OrderDetailsQueryHandler(IOrderRepository orderRepository, ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }
        public async Task<OrderDto> Handle(OrderDetailsQuery request, CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync("HANDLER"); await Console.Out.WriteLineAsync("HANDLER"); await Console.Out.WriteLineAsync("HANDLER"); await Console.Out.WriteLineAsync("HANDLER");
            var order = await _orderRepository.GetOrderById(request.OrderId);

            var orderDto = _mapper.Map<OrderDto>(order);

            return orderDto;
        }
    }
}
