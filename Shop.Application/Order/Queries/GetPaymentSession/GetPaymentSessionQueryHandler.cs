using MediatR;
using Shop.Application.Exceptions;
using Shop.Application.Services;
using Shop.Domain.Interfaces;
using Stripe.Checkout;

namespace Shop.Application.Order.Queries.GetPaymentSession
{
    public class GetPaymentSessionQueryHandler : IRequestHandler<GetPaymentSessionQuery, Session>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentService _paymentService;

        public GetPaymentSessionQueryHandler(IOrderRepository orderRepository, IPaymentService paymentService)
        {
            _orderRepository = orderRepository;
            _paymentService = paymentService;
        }
        public async Task<Session> Handle(GetPaymentSessionQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId);

            if (order == null)
            {
                throw new NotFoundException("Cart not found.");
            }

            Session session = _paymentService.CreatePaymentSessionForOrder(order);

            return session;
        }
    }
}
