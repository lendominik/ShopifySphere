using AutoMapper;
using MediatR;
using Shop.Application.Common;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Domain.Interfaces;

namespace Shop.Application.Order.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, PagedResult<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        
        public async Task<PagedResult<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            request.PageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            request.PageSize = request.PageSize < 1 ? 10 : request.PageSize;

            var orders = _orderRepository.GetAllOrders();

            if (orders == null)
            {
                throw new NotFoundException("Orders not found.");
            }

            if (!string.IsNullOrEmpty(request.SearchPhrase))
            {
                orders = orders.Where(r => r.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) || r.LastName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                r.Email.ToLower().Contains(request.SearchPhrase.ToLower()));
            }

            if(!string.IsNullOrEmpty(request.Status))
            {
                orders = orders.Where(r => r.OrderStatus.ToString() == request.Status);
            }

            orders = orders.OrderByDescending(order => order.OrderDate);

            var ordersCount = orders.Count();
            var ordersToDisplay = orders.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(ordersToDisplay);

            var result = new PagedResult<OrderDto>(orderDtos, ordersCount, request.PageSize, request.PageNumber);

            return result;
        }
    }
}
