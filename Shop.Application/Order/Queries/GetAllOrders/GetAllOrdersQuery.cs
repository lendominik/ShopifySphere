using MediatR;
using Shop.Domain.Entities;

namespace Shop.Application.Order.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<PagedResult>
    {
        public OrderStatus OrderStatus { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchPhrase { get; set; }
        public string? Status { get; set; }
    }
}
