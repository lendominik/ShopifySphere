using MediatR;

namespace Shop.Application.Order.Queries.GetUserOrders
{
    public class GetUserOrdersQuery : IRequest<List<OrderDto>>
    {

    }
}
