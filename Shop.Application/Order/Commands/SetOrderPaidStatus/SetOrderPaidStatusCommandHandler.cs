using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;

namespace Shop.Application.Order.Commands.SetOrderPaidStatus
{
    public class SetOrderPaidStatusCommandHandler : IRequestHandler<SetOrderPaidStatusCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public SetOrderPaidStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(SetOrderPaidStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId);

            if (order == null)
            {
                throw new NotFoundException("Order not found.");
            }

            await _orderRepository.SetOrderPaidStauts(order);

            return Unit.Value;
        }
    }
}
