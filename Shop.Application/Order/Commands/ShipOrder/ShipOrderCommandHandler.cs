using MediatR;
using Shop.Application.ApplicationUser;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;

namespace Shop.Application.Order.Commands.ShipOrder
{
    public class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserContext _userContext;

        public ShipOrderCommandHandler(IUserContext userContext, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _userContext = userContext;
        }
        public async Task<Unit> Handle(ShipOrderCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            var isEdibable = user != null && (user.IsInRole("Owner"));

            if (!isEdibable)
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
