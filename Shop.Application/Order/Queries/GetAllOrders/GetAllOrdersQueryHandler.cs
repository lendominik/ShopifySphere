using AutoMapper;
using MediatR;
using Shop.Application.Common.PagedResult;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Application.Services.OrderServices;
using Shop.Domain.Interfaces;

namespace Shop.Application.Order.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, OrderPagedResult<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IOrderFilterService _orderFilterService;
        private readonly IPaginationService _paginationService;

        public GetAllOrdersQueryHandler(IPaginationService paginationService, IOrderFilterService orderFilterService, IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderFilterService = orderFilterService;
            _paginationService = paginationService;
        }
        
        public async Task<OrderPagedResult<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = _orderRepository.GetAllOrders();

            if (orders == null)
            {
                throw new NotFoundException("Orders not found.");
            }

            orders = _orderFilterService.FilterBySearchPhrase(orders, request.SearchPhrase);

            orders = _orderFilterService.FilterByStatus(orders, request.Status);

            orders = orders.OrderByDescending(order => order.OrderDate);

            var ordersCount = orders.Count();

            var ordersToDisplay = _paginationService.PaginationSkipAndTake(orders, request.PageNumber, request.PageSize);

            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(ordersToDisplay);

            var result = new OrderPagedResult<OrderDto>(orderDtos, ordersCount, request.PageSize, request.PageNumber);

            return result;
        }
    }
}
