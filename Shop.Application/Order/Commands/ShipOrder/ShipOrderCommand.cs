using MediatR;

namespace Shop.Application.Order.Commands.ShipOrder
{
    public class ShipOrderCommand : IRequest
    {
        public int OrderId { get; set; }
    }
}
