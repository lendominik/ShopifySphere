using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;

namespace Shop.Application.Order.Commands.CompleteOrderCommand
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public CompleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Unit> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId);

            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            await _orderRepository.CompleteOrder(order);

            return Unit.Value;
        }
    }
}
