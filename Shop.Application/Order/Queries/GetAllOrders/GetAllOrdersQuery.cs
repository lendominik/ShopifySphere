using MediatR;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Order.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<PagedResult>
    {
        public OrderStatus OrderStatus { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchPhrase { get; set; }
    }
}
