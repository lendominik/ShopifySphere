using AutoMapper;
using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;

namespace Shop.Application.Order.Queries.OrderDetails
{
    public class OrderDetailsQueryHandler : IRequestHandler<OrderDetailsQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderDetailsQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<OrderDto> Handle(OrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId);

            if(order == null || order.CartItems == null)
            {
                throw new NotFoundException("Order not found.");
            }

            var orderDto = _mapper.Map<OrderDto>(order);

            return orderDto;
        }
    }
}
