using MediatR;
using Shop.Application.ApplicationUser;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Domain.Interfaces;

namespace Shop.Application.Order.Commands.ShipOrder
{
    public class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAccessControlService _accessControlService;

        public ShipOrderCommandHandler(IOrderRepository orderRepository, IAccessControlService accessControlService)
        {
            _orderRepository = orderRepository;
            _accessControlService = accessControlService;
        }
        public async Task<Unit> Handle(ShipOrderCommand request, CancellationToken cancellationToken)
        {
            if(!_accessControlService.IsEditable())
            {
                return Unit.Value;
            }

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
