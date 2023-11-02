using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Application.ApplicationUser;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Order.Queries.GetUserOrders
{
    public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<OrderDto>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userEmail = user.FindFirstValue(ClaimTypes.Email);

            var orders = await _orderRepository.GetUserOrders(userEmail);

            var orderDtos = _mapper.Map<List<OrderDto>>(orders);

            return orderDtos;
        }
    }
}
