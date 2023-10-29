using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart.Queries.GetCart
{
    public class GetCartQuery : IRequest<CartDto>
    {
    }
}
