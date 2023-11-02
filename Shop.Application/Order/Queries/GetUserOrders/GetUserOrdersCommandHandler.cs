﻿using AutoMapper;
using MediatR;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Order.Queries.GetUserOrders
{
    public class GetUserOrdersCommandHandler : IRequestHandler<GetUserOrdersCommand, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetUserOrdersCommandHandler(IOrderRepository orderRepository, IMapper mapper) 
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> Handle(GetUserOrdersCommand request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetUserOrders(request.Email);

            var orderDtos = _mapper.Map<List<OrderDto>>(orders);

            return orderDtos;
        }
    }
}
