using MediatR;

namespace Shop.Application.Order.Commands.CompleteOrderCommand
{
    public class CompleteOrderCommand : IRequest
    {
        public int OrderId { get; set; }
    }
}
