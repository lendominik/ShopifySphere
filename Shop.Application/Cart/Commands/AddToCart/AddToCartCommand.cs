using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Cart.Commands.AddToCart
{
    public class AddToCartCommand : IRequest
    {
        public int Quantity { get; set; }
        public string UnitPrice { get; set; }
        public string EncodedName { get; set; }
    }
}
