using Shop.Domain.Entities;
using Stripe.Checkout;

namespace Shop.Application.Services.PaymentService
{
    public interface IPaymentSessionOptionsService
    {
        SessionCreateOptions GetSessionCreateOptions(List<CartItem> productList, string domain);
    }
    public class PaymentSessionOptionsService : IPaymentSessionOptionsService
    {
        public SessionCreateOptions GetSessionCreateOptions(List<CartItem> productList, string domain)
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
