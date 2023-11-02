using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Order.Queries.GetUserOrders
{
    public class GetUserOrdersQuery : IRequest<List<OrderDto>>
    {
        public string Email { get; set; }
    }
}
