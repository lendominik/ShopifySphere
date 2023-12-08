using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;

namespace Shop.Application.Order.Commands.ShipOrder
{
    public class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public ShipOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Unit> Handle(ShipOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId);

            if(order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            await _orderRepository.ShipOrder(order);

            return Unit.Value;
        }
    }
}
