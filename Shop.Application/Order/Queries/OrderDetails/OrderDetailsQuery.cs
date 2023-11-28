using MediatR;

namespace Shop.Application.Order.Queries.OrderDetails
{
    public class OrderDetailsQuery : IRequest<OrderDto>
    {
        public int OrderId { get; set; }
        public OrderDetailsQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
