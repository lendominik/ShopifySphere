using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;
using Stripe.Checkout;

namespace Shop.Application.Order.Queries.GetPaymentSession
{
    public class GetPaymentSessionQueryHandler : IRequestHandler<GetPaymentSessionQuery, Session>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly string _domain;

        public GetPaymentSessionQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _domain = "https://localhost:7109/";
        }
        public async Task<Session> Handle(GetPaymentSessionQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId);

            if (order == null)
            {
                throw new NotFoundException("Cart not found.");
            }

            var productList = order.CartItems;

            var options = new SessionCreateOptions
            {
                SuccessUrl = _domain + "Order/Success",
                CancelUrl = _domain + "Order/Cancel",
                LineItems = productList.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)item.Item.Price * 100,
                        Currency = "pln",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Item.Name,
                            Description = item.Item.Description
                        },
                    },
                    Quantity = item.Quantity
                }).ToList(),
                Mode = "payment"
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return session;
        }
    }
}
