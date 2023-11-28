using MediatR;

namespace Shop.Application.Order.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest
    {
        public int OrderId { get; set; }
    }
}
