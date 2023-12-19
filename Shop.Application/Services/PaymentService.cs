using Shop.Domain.Entities;
using Stripe.Checkout;

namespace Shop.Application.Services
{
    public interface IPaymentService
    {
        Session CreatePaymentSessionForOrder(Domain.Entities.Order order, string domain);
    }

    public class PaymentService : IPaymentService
    {
        private readonly SessionService _sessionService;

        public PaymentService(SessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public Session CreatePaymentSessionForOrder(Domain.Entities.Order order, string domain)
        {
            var productList = order.CartItems;

            var options = GetSessionCreateOptions(productList, domain);

            Session session = _sessionService.Create(options);

            return session;
        }

        private SessionCreateOptions GetSessionCreateOptions(List<CartItem> productList, string domain)
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "/Order/Success",
                CancelUrl = domain + "/Order/Cancel",
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

            return options;
        }
    }
}
