using MediatR;
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
            await _orderRepository.CompleteOrder(request.OrderId);

            return Unit.Value;
        }
    }
}
