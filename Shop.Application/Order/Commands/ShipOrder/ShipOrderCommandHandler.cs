﻿using MediatR;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Order.Commands.ShipOrder
{
    public class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public ShipOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Unit> Handle(ShipOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderRepository.ShipOrder(request.OrderId);

            return Unit.Value;
        }
    }
}