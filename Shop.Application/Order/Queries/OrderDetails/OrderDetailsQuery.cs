using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
