using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Application.Exceptions;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Shop.Application.Order.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, PagedResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            request.PageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            request.PageSize = request.PageSize < 1 ? 10 : request.PageSize;

            var orders = await _orderRepository.GetAllOrders();

            if (orders == null)
            {
                throw new NotFoundException("Nie ma żadnych zamówień w historii.");
            }

            if (!string.IsNullOrEmpty(request.SearchPhrase))
            {
                orders = orders.Where(r => r.FirstName.ToLower().Contains(request.SearchPhrase.ToLower()) || r.LastName.ToLower().Contains(request.SearchPhrase.ToLower()) ||
                r.Email.ToLower().Contains(request.SearchPhrase.ToLower())).ToList();
            }

            if(!string.IsNullOrEmpty(request.Status))
            {
                orders = orders.Where(r => r.OrderStatus.ToString() == request.Status).ToList();
            }


            orders = orders.OrderByDescending(order => order.OrderDate).ToList();

            var ordersCount = orders.Count();
            var ordersToDisplay = orders.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();

            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(ordersToDisplay);

            var result = new PagedResult(orderDtos, ordersCount, request.PageSize, request.PageNumber);

            return result;
        }
    }
}
