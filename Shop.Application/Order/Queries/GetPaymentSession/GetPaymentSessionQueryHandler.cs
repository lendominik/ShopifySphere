using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Interfaces;
using Stripe.Checkout;

namespace Shop.Application.Order.Queries.GetPaymentSession
{
    public class GetPaymentSessionQueryHandler : IRequestHandler<GetPaymentSessionQuery, Session>
    {
        private readonly IOrderRepository _orderRepository;

        public GetPaymentSessionQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Session> Handle(GetPaymentSessionQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId);

            if (order == null)
            {
                throw new NotFoundException("Nie znaleziono podanego koszyka.");
            }

            var domain = "https://localhost:7109/";

            var productList = order.CartItems;

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "Order/Success",
                CancelUrl = domain + "Order/Cancel",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"
            };

            foreach (var item in productList)
            {
                var sessionListItem = new SessionLineItemOptions
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
                };
                options.LineItems.Add(sessionListItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            return session;
        }
    }
}
