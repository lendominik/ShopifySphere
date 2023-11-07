using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe.Checkout;

namespace Shop.Application.Order.Queries.GetPaymentSession
{
    public class GetPaymentSessionQuery : IRequest<Session>
    {
        public int OrderId { get; set; }
    }
}
