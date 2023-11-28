using MediatR;

namespace Shop.Application.Order.Commands.CreateOrder
{
    public class CreateOrderCommand : OrderDto, IRequest
    {
    }
}
