using Stripe.Checkout;

namespace Shop.Application.Services.PaymentService
{
    public interface IPaymentService
    {
        Session CreatePaymentSessionForOrder(Domain.Entities.Order order, string domain);
    }

    public class PaymentService : IPaymentService
    {
        private readonly IPaymentSessionOptionsService _paymentSessionOptionsService;

        public PaymentService(IPaymentSessionOptionsService paymentSessionOptionsService)
        {
            _paymentSessionOptionsService = paymentSessionOptionsService;
        }
        public Session CreatePaymentSessionForOrder(Domain.Entities.Order order, string domain)
        {
            var productList = order.CartItems;

            var options = _paymentSessionOptionsService.GetSessionCreateOptions(productList, domain);

            var service = new SessionService();
            Session session = service.Create(options);

            return session;
        }
    }
}
