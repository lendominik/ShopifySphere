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
        private readonly IUserContext _userContext;

        public ShipOrderCommandHandler(IOrderRepository orderRepository, IAccessControlService accessControlService, IUserContext userContext)
        {
            _orderRepository = orderRepository;
            _accessControlService = accessControlService;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(ShipOrderCommand request, CancellationToken cancellationToken)
        {
            if(!_accessControlService.IsEditable(_userContext))
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
