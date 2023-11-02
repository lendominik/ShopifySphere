using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Order.Commands.CancelOrder
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public CancelOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId);

            if(order == null)
            {
                throw new NotFoundException("Zamówienie o podanym ID nie istnieje.");
            }

            if(order.OrderStatus != Domain.Entities.OrderStatus.Pending)
            {
                throw new Exception("Nie można anulować zamówienia będącego w trakcie realizacji.");
            }

            await _orderRepository.CancelOrder(request.OrderId);

            return Unit.Value;
        }
    }
}
