using MediatR;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Order.Commands.SetOrderPaidStatus
{
    public class SetOrderPaidStatusCommandHandler : IRequestHandler<SetOrderPaidStatusCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public SetOrderPaidStatusCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(SetOrderPaidStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId);

            await _orderRepository.SetOrderPaidStauts(order);

            return Unit.Value;
        }
    }
}
