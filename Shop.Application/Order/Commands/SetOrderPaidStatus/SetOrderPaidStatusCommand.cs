using MediatR;

namespace Shop.Application.Order.Commands.SetOrderPaidStatus
{
    public class SetOrderPaidStatusCommand : IRequest
    {
        public int OrderId { get; set; }
    }
}
