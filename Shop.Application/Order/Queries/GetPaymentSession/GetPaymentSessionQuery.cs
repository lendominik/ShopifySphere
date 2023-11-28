using MediatR;
using Stripe.Checkout;

namespace Shop.Application.Order.Queries.GetPaymentSession
{
    public class GetPaymentSessionQuery : IRequest<Session>
    {
        public int OrderId { get; set; }
    }
}
