using Shop.Domain.Entities;
using Stripe.Checkout;

namespace Shop.Application.Services
{
    public interface IPaymentService
    {
        Stripe.Checkout.Session CreatePaymentSessionForOrder(Domain.Entities.Order order);
    }

    public class PaymentService : IPaymentService
    {
        private readonly string _domain;

        public PaymentService()
        {
            _domain = "https://localhost:7109/";
        }
        public Stripe.Checkout.Session CreatePaymentSessionForOrder(Domain.Entities.Order order)
        {
            var productList = order.CartItems;

            var options = GetSessionCreateOptions(productList);

            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);

            return session;
        }

        private Stripe.Checkout.SessionCreateOptions GetSessionCreateOptions(List<CartItem> productList)
        {
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = _domain + "/Order/Success",
                CancelUrl = _domain + "/Order/Cancel",
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
