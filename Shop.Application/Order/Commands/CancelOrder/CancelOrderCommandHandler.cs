using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;

namespace Shop.Application.Order.Commands.CancelOrder
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public CancelOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId);

            if(order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            if(order.OrderStatus != Domain.Entities.OrderStatus.Pending)
            {
                throw new Exception("You cannot cancel an order that is currently being processed.");
            }

            await _orderRepository.CancelOrder(request.OrderId);

            return Unit.Value;
        }
    }
}
