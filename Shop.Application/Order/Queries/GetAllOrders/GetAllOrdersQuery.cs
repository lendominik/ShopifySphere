using MediatR;
using Shop.Application.Common.PagedResult;
using Shop.Domain.Entities;

namespace Shop.Application.Order.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<OrderPagedResult<OrderDto>>
    {
        public OrderStatus OrderStatus { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchPhrase { get; set; }
        public string? Status { get; set; }
    }
}
